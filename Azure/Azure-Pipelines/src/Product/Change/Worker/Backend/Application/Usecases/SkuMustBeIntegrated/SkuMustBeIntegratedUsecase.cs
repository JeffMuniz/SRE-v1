using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Product.Change.Worker.Backend.Application.Usecases.Shared.Configurations;
using Product.Change.Worker.Backend.Domain.Repositories;
using Product.Change.Worker.Backend.Domain.Services;
using System.Threading;
using System.Threading.Tasks;
using SharedDomain = Shared.Backend.Domain;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Change.Worker.Backend.Application.Usecases.SkuMustBeIntegrated
{
    public class SkuMustBeIntegratedUsecase : ISkuMustBeIntegratedUsecase
    {
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<IntegrationSkuConfigurationOptions> _integrationSkuConfigurationOptions;
        private readonly ISkuIntegrationRepository _skuIntegrationRepository;
        private readonly ISkuNotificationService _skuNotificationService;

        public SkuMustBeIntegratedUsecase(
            IMapper mapper,
            IOptionsMonitor<IntegrationSkuConfigurationOptions> integrationSkuConfigurationOptions,
            ISkuIntegrationRepository skuIntegrationRepository,
            ISkuNotificationService skuNotificationService
        )
        {
            _mapper = mapper;
            _integrationSkuConfigurationOptions = integrationSkuConfigurationOptions;
            _skuIntegrationRepository = skuIntegrationRepository;
            _skuNotificationService = skuNotificationService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var supplierSkuIdResult = Maybe.From(_mapper.Map<SharedDomain.ValueObjects.SupplierSkuId>(inbound))
                .ToResult(Domain.ValueObjects.ErrorType.InvalidInput);
            if (supplierSkuIdResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(supplierSkuIdResult.Error);

            var existingSkuIntegrationResult = await _skuIntegrationRepository.Get(supplierSkuIdResult.Value, cancellationToken)
                .ToResult(Domain.ValueObjects.ErrorType.NotFound);
            if (existingSkuIntegrationResult.IsFailure)
            {
                if (!inbound.Active.HasValue || inbound.Active.Value)
                    return Models.Outbound.CreateMustIntegrated();

                return Models.Outbound.CreateNotMustIntegrated();
            }

            var existingSkuIntegration = existingSkuIntegrationResult.Value;
            var newSkuPrice = Maybe.From(_mapper.Map<SharedDomain.ValueObjects.Price>(inbound.Price))
                .ToResult(Domain.ValueObjects.ErrorType.InvalidInput);
            if (newSkuPrice.IsSuccess &&
                newSkuPrice.Value.For > 0 &&
                existingSkuIntegration.ChangePrice(newSkuPrice.Value).IsSuccess)
            {
                await _skuIntegrationRepository.Update(existingSkuIntegration, cancellationToken);
                await _skuNotificationService.NotifyChangedPrice(existingSkuIntegration, cancellationToken);
            }

            var changeActiveResult = Maybe.From(inbound.Active)
                .ToResult(Domain.ValueObjects.ErrorType.InvalidInput)
                .Bind(active => existingSkuIntegration.ChangeActive(active.Value));
            if (changeActiveResult.IsSuccess && !existingSkuIntegration.SupplierSku.Active)
            {
                await _skuIntegrationRepository.Update(existingSkuIntegration, cancellationToken);
                await _skuNotificationService.NotifyRemoveSku(existingSkuIntegration, cancellationToken);

                return Models.Outbound.CreateNotMustIntegrated();
            }

            var timeToCheckChangesInExistingSkus = _integrationSkuConfigurationOptions.CurrentValue.Changes.TimeToCheckChangesInExistingSkus;
            var mustBeCheckChangesResult = existingSkuIntegration.MustBeCheckChanges(timeToCheckChangesInExistingSkus);
            if (existingSkuIntegration.SupplierSku.Active &&
                (mustBeCheckChangesResult.IsSuccess || changeActiveResult.IsSuccess)
            )
                return Models.Outbound.CreateMustIntegrated();

            return Models.Outbound.CreateNotMustIntegrated();
        }
    }
}
