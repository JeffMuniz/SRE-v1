using CSharpFunctionalExtensions;
using Integration.Api.Backend.Domain.Entities;
using Integration.Api.Backend.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.ImportCatalog
{
    internal class ImportCatalogUseCase : IImportCatalogUseCase
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOfferNotificationRepository _offerNotificationRepository;
        private readonly ICatalogImportSettingsRepository _catalogImportSettingsRepository;

        public ImportCatalogUseCase(
            ILogger<ImportCatalogUseCase> logger,
            IServiceProvider serviceProvider,
            IOfferNotificationRepository offerNotificationRepository,
            ICatalogImportSettingsRepository catalogImportSettingsRepository
        )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _offerNotificationRepository = offerNotificationRepository;
            _catalogImportSettingsRepository = catalogImportSettingsRepository;
        }

        public async Task Execute(int batchSize, int degreeOfParallelism, CancellationToken cancellationToken)
        {
            batchSize = batchSize > 0 ? batchSize : 1;
            degreeOfParallelism = degreeOfParallelism > 0 ? degreeOfParallelism : 1;
            var catalogImportSettings = await _catalogImportSettingsRepository.Get(cancellationToken)
                ?? new CatalogImportSettings();

            async Task<IEnumerable<OfferNotification>> ParallellGetNextBatchImport()
            {
                var tasks = Task.WhenAll(
                    Enumerable.Range(0, degreeOfParallelism)
                    .Select(part => catalogImportSettings.ImportedOffersCount + batchSize * part)
                    .Select(async offset =>
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var offerNotificationRepository = scope.ServiceProvider.GetRequiredService<IOfferNotificationRepository>();
                        return await offerNotificationRepository.GetNextBatchImport(offset, batchSize, cancellationToken);
                    })
                );

                if (await Task.WhenAny(tasks, Task.Delay(Timeout.Infinite, cancellationToken)) != tasks)
                    return Enumerable.Empty<OfferNotification>();

                var result = await tasks;

                return result
                    .SelectMany(x => x)
                    .ToList();
            }

            var elapsedTime = Stopwatch.StartNew();

            while (
                !cancellationToken.IsCancellationRequested &&
                await ParallellGetNextBatchImport() is IEnumerable<OfferNotification> offersNotification &&
                offersNotification.Any()
            )
            {
                await _offerNotificationRepository.BulkAdd(offersNotification, cancellationToken);

                catalogImportSettings.AddImportedOffersCount(offersNotification.Count());

                await _catalogImportSettingsRepository.Save(catalogImportSettings, cancellationToken);

                _logger.LogInformation($"Imported offers batch offset: {catalogImportSettings.ImportedOffersCount}, batch size: {offersNotification.Count()} at {elapsedTime.Elapsed}");

                elapsedTime.Restart();
            }
        }
    }
}
