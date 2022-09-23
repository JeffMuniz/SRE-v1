using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Repositories;
using Availability.Manager.Worker.Backend.Domain.Services;
using Availability.Manager.Worker.Configurations.Models;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using AvailabilityFoundModels = Availability.Manager.Worker.Backend.Application.UseCases.AvailabilityFound.Models;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Manager.Worker.Backend.Application.UseCases.AvailabilityFound
{
    public class AvailabilityFoundUseCase : IAvailabilityFoundUseCase
    {
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<RedisShardConfigurationOptions> _shardOptions;
        private readonly ISkuRepository _skuRepository;
        private readonly ICashCacheService _cashCacheService;
        private readonly IPointCacheService _pointCacheService;
        private readonly IAvailabilityNotificationService _skuNotificationService;
        private readonly ICacheRenewScheduleService _renewCacheScheduleService;

        public AvailabilityFoundUseCase(
            IMapper mapper,
            IOptionsMonitor<RedisShardConfigurationOptions> shardOptions,
            ISkuRepository skuRepository,
            ICashCacheService cashCacheService,
            IPointCacheService pointCacheService,
            IAvailabilityNotificationService skuNotificationService,
            ICacheRenewScheduleService renewCacheScheduleService
        )
        {
            _mapper = mapper;
            _shardOptions = shardOptions;
            _skuRepository = skuRepository;
            _cashCacheService = cashCacheService;
            _pointCacheService = pointCacheService;
            _skuNotificationService = skuNotificationService;
            _renewCacheScheduleService = renewCacheScheduleService;
        }

        public async Task<Result<AvailabilityFoundModels.Outbound, SharedUsecases.Models.Error>> Execute(
            AvailabilityFoundModels.Inbound inbound,
            CancellationToken cancellationToken
        )
        {
            var skuAvailabilityResult = Maybe.From(_mapper.Map<Domain.Entities.SkuAvailability>(inbound))
                .ToResult(Domain.ValueObjects.ErrorType.InvalidInput);
            if (skuAvailabilityResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(skuAvailabilityResult.Error);

            var skuAvailability = skuAvailabilityResult.Value;
            var shardId = Maybe.From(_mapper.Map<Domain.ValueObjects.ShardId>(inbound))
                .GetValueOrDefault(new Domain.ValueObjects.ShardId(_shardOptions.CurrentValue.DefaultInstanceName));

            if (
                await _skuRepository.Get(skuAvailability.Id, cancellationToken) is var existingSkuResult &&
                existingSkuResult.HasNoValue
            )
            {
                await _skuRepository.Add(skuAvailability, cancellationToken);

                await _cashCacheService.Add(shardId, skuAvailability, cancellationToken);

                await _pointCacheService.Remove(shardId, skuAvailability, cancellationToken);

                await _skuNotificationService.NotifyAvailabilityChanged(skuAvailability, cancellationToken);
            }
            else if (
                existingSkuResult.Value is var existingSku &&
                existingSku.ChangeLatestPartnerAvailabilityFoundDate(skuAvailability.LatestPartnerAvailabilityFoundDate) is var changeLatestPartnerAvailabilityFoundDateResult &&
                existingSku.ChangeAll(skuAvailability) is var changeAllResult &&
                changeAllResult.IsFailure
            )
            {
                if (changeLatestPartnerAvailabilityFoundDateResult.IsSuccess)
                    await _skuRepository.UpdateOnlyLatestPartnerAvailabilityFoundDate(existingSku, cancellationToken);

                await _cashCacheService.Add(shardId, existingSku, cancellationToken);
            }
            else
            {
                var changeAllSuccessResult = changeAllResult.Value;

                await _skuRepository.Update(existingSku, cancellationToken);

                await _cashCacheService.Add(shardId, existingSku, cancellationToken);

                await _pointCacheService.Remove(shardId, existingSku, cancellationToken);

                if (changeAllSuccessResult.Available.IsSuccess || changeAllSuccessResult.Price.IsSuccess)
                    await _skuNotificationService.NotifyAvailabilityChanged(existingSku, cancellationToken);
            }

            await _renewCacheScheduleService.Schedule(shardId, skuAvailability, cancellationToken);

            return AvailabilityFoundModels.Outbound.Create();
        }
    }
}
