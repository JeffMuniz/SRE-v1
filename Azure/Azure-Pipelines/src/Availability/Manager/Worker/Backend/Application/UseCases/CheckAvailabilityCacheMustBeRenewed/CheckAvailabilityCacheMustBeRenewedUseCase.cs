using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Services;
using Availability.Manager.Worker.Configurations.Models;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using CheckAvailabilityModels = Availability.Manager.Worker.Backend.Application.UseCases.CheckAvailabilityCacheMustBeRenewed.Models;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Manager.Worker.Backend.Application.UseCases.CheckAvailabilityCacheMustBeRenewed
{
    public class CheckAvailabilityCacheMustBeRenewedUseCase : ICheckAvailabilityCacheMustBeRenewedUseCase
    {
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<RedisShardConfigurationOptions> _shardOptions;
        private readonly ICashCacheService _cashCacheService;
        private readonly IPointCacheService _pointCacheService;
        private readonly IAvailabilityNotificationService _skuNotificationService;

        public CheckAvailabilityCacheMustBeRenewedUseCase(
            IMapper mapper,
            IOptionsMonitor<RedisShardConfigurationOptions> shardOptions,
            ICashCacheService cashCacheService,
            IPointCacheService pointCacheService,
            IAvailabilityNotificationService skuNotificationService
        )
        {
            _mapper = mapper;
            _shardOptions = shardOptions;
            _cashCacheService = cashCacheService;
            _pointCacheService = pointCacheService;
            _skuNotificationService = skuNotificationService;
        }

        public async Task<Result<CheckAvailabilityModels.Outbound, SharedUsecases.Models.Error>> Execute(
            CheckAvailabilityModels.Inbound inbound,
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

            if (await _cashCacheService.CheckInUse(shardId, skuAvailability, cancellationToken) ||
                await _pointCacheService.CheckInUse(shardId, skuAvailability, cancellationToken)
            )
                await _skuNotificationService.SendGetLatestAvailability(shardId, skuAvailability, cancellationToken);

            return CheckAvailabilityModels.Outbound.Create();
        }
    }
}
