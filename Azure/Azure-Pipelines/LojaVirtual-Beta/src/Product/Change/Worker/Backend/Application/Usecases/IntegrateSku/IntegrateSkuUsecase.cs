using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Product.Change.Worker.Backend.Application.Usecases.Shared.Configurations;
using Product.Change.Worker.Backend.Domain.Repositories;
using Product.Change.Worker.Backend.Domain.Services;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Change.Worker.Backend.Application.Usecases.IntegrateSku
{
    public class IntegrateSkuUsecase : IIntegrateSkuUsecase
    {
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<IntegrationSkuConfigurationOptions> _integrationSkuConfigurationOptions;
        private readonly ISkuIntegrationRepository _skuIntegrationRepository;
        private readonly ISkuNotificationService _skuNotificationService;
        private readonly ICrcHashProviderService _crcHashProviderService;

        public IntegrateSkuUsecase(
            IMapper mapper,
            IOptionsMonitor<IntegrationSkuConfigurationOptions> integrationSkuConfigurationOptions,
            ISkuIntegrationRepository skuIntegrationRepository,
            ISkuNotificationService skuNotificationService,
            ICrcHashProviderService crcHashProviderService
        )
        {
            _mapper = mapper;
            _integrationSkuConfigurationOptions = integrationSkuConfigurationOptions;
            _skuIntegrationRepository = skuIntegrationRepository;
            _skuNotificationService = skuNotificationService;
            _crcHashProviderService = crcHashProviderService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var supplierSkuResult = Maybe.From(_mapper.Map<Domain.Entities.SupplierSku>(inbound))
                .ToResult(Domain.ValueObjects.ErrorType.InvalidInput);
            if (supplierSkuResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(supplierSkuResult.Error);

            var supplierSku = supplierSkuResult.Value;
            var existingSkuIntegrationResult = await _skuIntegrationRepository.Get(supplierSku.Id, cancellationToken)
                .ToResult(Domain.ValueObjects.ErrorType.NotFound);
            if (existingSkuIntegrationResult.IsFailure)
            {
                if (!supplierSku.Active)
                    return _mapper.Map<SharedUsecases.Models.Error>(Domain.ValueObjects.ErrorType.InvalidInput);

                var newSkuIntegration = Domain.Entities.SkuIntegration
                    .Create(supplierSku, _crcHashProviderService);

                await _skuIntegrationRepository.Add(newSkuIntegration, cancellationToken);
                await _skuNotificationService.NotifyAddSku(newSkuIntegration, cancellationToken);

                return Models.Outbound.Create();
            }

            var existingSkuIntegration = existingSkuIntegrationResult.Value;
            var changePriceResult = existingSkuIntegration.ChangePrice(supplierSku.Price);
            var changeActiveResult = existingSkuIntegration.ChangeActive(supplierSku.Active);
            var timeToCheckChangesInExistingSkus = _integrationSkuConfigurationOptions.CurrentValue.Changes.TimeToCheckChangesInExistingSkus;
            var mustBeCheckChangesResult = existingSkuIntegration
                .MustBeCheckChanges(timeToCheckChangesInExistingSkus);
            var changesResult = mustBeCheckChangesResult
                .Bind(skuIntegration => skuIntegration.ChangeSupplierSku(supplierSku, _crcHashProviderService));

            if (mustBeCheckChangesResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(Domain.ValueObjects.ErrorType.ThereIsNoChange);

            existingSkuIntegration.UpdateLastIntegratedNow();
            await _skuIntegrationRepository.Update(existingSkuIntegration, cancellationToken);

            if (changePriceResult.IsSuccess)
                await _skuNotificationService.NotifyChangedPrice(existingSkuIntegration, cancellationToken);

            if (changeActiveResult.IsSuccess && !existingSkuIntegration.SupplierSku.Active)
                await _skuNotificationService.NotifyRemoveSku(existingSkuIntegration, cancellationToken);

            if (changesResult.IsSuccess || (changeActiveResult.IsSuccess && existingSkuIntegration.SupplierSku.Active))
                await _skuNotificationService.NotifyUpdateSku(existingSkuIntegration, cancellationToken);

            if (changePriceResult.IsSuccess || changeActiveResult.IsSuccess || changesResult.IsSuccess)
                return Models.Outbound.Create();

            return _mapper.Map<SharedUsecases.Models.Error>(Domain.ValueObjects.ErrorType.ThereIsNoChange);
        }
    }
}
