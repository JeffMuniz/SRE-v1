using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Repositories;
using Availability.Manager.Worker.Backend.Domain.Services;
using Availability.Manager.Worker.Configurations.Models;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Manager.Worker.Backend.Application.UseCases.GetLatestAvailability
{
    public class GetLatestAvailabilityUseCase : IGetLatestAvailabilityUseCase
    {
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<SearchConfigurationOptions> _searchOptions;
        private readonly IOptionsMonitor<RedisShardConfigurationOptions> _shardOptions;
        private readonly ISkuRepository _skuRepository;
        private readonly ICashCacheService _cashCacheService;
        private readonly IAvailabilityNotificationService _skuNotificationService;
        private readonly ICacheRenewScheduleService _renewCacheScheduleService;

        public GetLatestAvailabilityUseCase(
            IMapper mapper,
            IOptionsMonitor<SearchConfigurationOptions> searchOptions,
            IOptionsMonitor<RedisShardConfigurationOptions> shardOptions,
            ISkuRepository skuRepository,
            ICashCacheService cashCacheService,
            IAvailabilityNotificationService skuNotificationService,
            ICacheRenewScheduleService renewCacheScheduleService
        )
        {
            _mapper = mapper;
            _searchOptions = searchOptions;
            _shardOptions = shardOptions;
            _skuRepository = skuRepository;
            _cashCacheService = cashCacheService;
            _skuNotificationService = skuNotificationService;
            _renewCacheScheduleService = renewCacheScheduleService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var skuAvailabilityIdResult = Maybe.From(_mapper.Map<Domain.ValueObjects.SkuAvailabilityId>(inbound))
                .ToResult(Domain.ValueObjects.ErrorType.InvalidInput);
            if (skuAvailabilityIdResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(skuAvailabilityIdResult.Error);

            var skuAvailabilityId = skuAvailabilityIdResult.Value;
            var shardId = Maybe.From(_mapper.Map<Domain.ValueObjects.ShardId>(inbound))
                .GetValueOrDefault(new Domain.ValueObjects.ShardId(_shardOptions.CurrentValue.DefaultInstanceName));

            if (await _skuRepository.Get(skuAvailabilityId, cancellationToken) is var getSkuAvailabilityResult &&
                (
                    getSkuAvailabilityResult.HasNoValue ||
                    getSkuAvailabilityResult.Value.PersistedSkuId != inbound.PersistedSkuId
                )
            )
                return _mapper.Map<SharedUsecases.Models.Error>(Domain.ValueObjects.ErrorType.NotFound);

            var existingSkuAvailability = getSkuAvailabilityResult.Value;

            await _cashCacheService.Add(shardId, existingSkuAvailability, cancellationToken);

            await _renewCacheScheduleService.Schedule(shardId, existingSkuAvailability, cancellationToken);

            if (existingSkuAvailability.MustBeGetPartnerAvailability(_searchOptions.CurrentValue.MinGetPartnerAvailabilityTime))
                await _skuNotificationService.SendGetAvailability(shardId, existingSkuAvailability, cancellationToken);

            return _mapper.Map<Models.Outbound>(existingSkuAvailability);
        }
    }
}
