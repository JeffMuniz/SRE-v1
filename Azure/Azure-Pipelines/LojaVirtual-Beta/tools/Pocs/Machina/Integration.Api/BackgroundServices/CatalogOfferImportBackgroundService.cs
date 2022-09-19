using Integration.Api.Backend.Application.Offer.UseCases.ImportCatalog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.BackgroundServices
{
    public class CatalogOfferImportBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptionsMonitor<CatalogOfferImportBackgroundServiceOptions> _options;

        public CatalogOfferImportBackgroundService(
            ILogger<CatalogOfferImportBackgroundService> logger,
            IServiceProvider serviceProvider,
            IOptionsMonitor<CatalogOfferImportBackgroundServiceOptions> options
        )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_options.CurrentValue.Enabled)
                return;

            try
            {
                _logger.LogInformation($"Executing catalog offers import at {DateTime.Now}");

                using var scope = _serviceProvider.CreateScope();
                var importCatalogUseCase = scope.ServiceProvider.GetRequiredService<IImportCatalogUseCase>();

                await importCatalogUseCase.Execute(_options.CurrentValue.BatchSize, _options.CurrentValue.DegreeOfParallelism, stoppingToken);

                _logger.LogInformation($"Catalog offers import completed at {DateTime.Now}");
            }
            catch (TaskCanceledException)
            {
                // No action required
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred executing catalog offers import at {DateTime.Now}");
            }
        }
    }
}
