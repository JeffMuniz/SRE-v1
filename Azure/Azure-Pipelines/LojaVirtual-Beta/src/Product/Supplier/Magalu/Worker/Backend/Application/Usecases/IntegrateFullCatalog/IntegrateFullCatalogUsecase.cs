using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Product.Supplier.Magalu.Worker.Backend.Application.Usecases.IntegrateFullCatalog.Configurations;
using Product.Supplier.Magalu.Worker.Backend.Application.Usecases.Shared.Resume;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SharedDomain = Product.Supplier.Shared.Worker.Backend.Domain;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Supplier.Magalu.Worker.Backend.Application.Usecases.IntegrateFullCatalog
{
    public class IntegrateFullCatalogUsecase : IIntegrateFullCatalogUsecase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<IntegrateFullCatalogOptions> _options;
        private readonly Domain.Services.IMagaluIntegrationService _magaluService;
        private readonly SharedDomain.Services.ISkuIntegrationService _skuIntegrationService;

        public IntegrateFullCatalogUsecase(
            ILogger<IntegrateFullCatalogUsecase> logger,
            IMapper mapper,
            IOptionsMonitor<IntegrateFullCatalogOptions> options,
            Domain.Services.IMagaluIntegrationService magaluService,
            SharedDomain.Services.ISkuIntegrationService skuIntegrationService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _options = options;
            _magaluService = magaluService;
            _skuIntegrationService = skuIntegrationService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando importação dos produtos do supplier {SupplierId}", inbound.SupplierId);

            try
            {
                var colors = await _magaluService.GetColors(cancellationToken)
                    .ToArrayAsync(cancellationToken);

                using var stoppingToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cancellationToken = stoppingToken.Token;

                var maxDegreeOfParallelism = _options.CurrentValue.MaxDegreeOfParallelism.GetValueOrDefault(1);
                var totalResume = new IntegratedResume(maxDegreeOfParallelism);

                var totalProducts = await _magaluService.GetCatalog(cancellationToken)
                    .ParallelForEachAsync(
                        maxDegreeOfParallelism: maxDegreeOfParallelism,
                        async skuMagalu =>
                        {
                            totalResume.Add(skuMagalu.Active.GetValueOrDefault());

                            var skuMustBeIntegrated = _mapper.Map<SharedDomain.ValueObjects.SkuMustBeIntegrated>(skuMagalu,
                                opt => opt.Items.Add("SupplierId", inbound.SupplierId));

                            var skuMustBeIntegratedResult = await _skuIntegrationService.SkuMustBeIntegrated(skuMustBeIntegrated, cancellationToken);
                            if (skuMustBeIntegratedResult.IsFailure)
                            {
                                _logger.LogError("O sku {SupplierSkuId} não pode ser integrado devido o erro: {Error}", skuMustBeIntegrated.SupplierSkuId, skuMustBeIntegratedResult.Error);
                                stoppingToken.Cancel();
                                return;
                            }

                            if (!skuMustBeIntegratedResult.Value)
                                return;

                            var specifications = await _magaluService.GetSpecifications(skuMagalu.Master, cancellationToken)
                                .ToArrayAsync(cancellationToken);

                            skuMagalu
                                .AssignColor(colors)
                                .AssignSpecifications(specifications)
                                .TryAssignDescriptionFromSpecifications(specifications);

                            var supplierSku = _mapper.Map<SharedDomain.Entities.SupplierSku>(skuMagalu,
                                opt => opt.Items.Add("SupplierId", inbound.SupplierId)
                            );
                            await _skuIntegrationService.IntegrateSku(supplierSku, cancellationToken);
                        },
                        cancellationToken
                    )
                    .CountAsync(cancellationToken);

                _logger.LogInformation("Importação dos produtos do supplier {SupplierId} finalizado com sucesso. Total: {total}, Resume: {totalResume} ", inbound.SupplierId, totalProducts, totalResume);

                return Models.Outbound.Create();
            }
            catch (Exception ex)
                when (ex is OperationCanceledException || ex is TaskCanceledException)
            {
                _logger.LogWarning(ex, "Processo de importação dos produtos do supplier {SupplierId} foi cancelado", inbound.SupplierId);
                return _mapper.Map<SharedUsecases.Models.Error>(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro no processo de importação dos produtos do supplier {SupplierId}", inbound.SupplierId);
                throw;
            }
        }
    }
}
