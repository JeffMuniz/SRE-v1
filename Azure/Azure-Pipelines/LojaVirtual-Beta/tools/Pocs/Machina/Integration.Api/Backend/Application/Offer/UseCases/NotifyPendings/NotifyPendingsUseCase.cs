using CSharpFunctionalExtensions;
using Integration.Api.Backend.Domain.Entities;
using Integration.Api.Backend.Domain.ExternalServices;
using Integration.Api.Backend.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.NotifyPendings
{
    public class NotifyPendingsUseCase : INotifyPendingsUseCase
    {
        private readonly ILogger _logger;
        private readonly IOfferNotificationRepository _offerNotificationRepository;
        private readonly IOfferNotificationHistoryRepository _offerNotificationHistoryRepository;
        private readonly IMacnaimaService _macnaimaService;

        public NotifyPendingsUseCase(
            ILogger<NotifyPendingsUseCase> logger,
            IOfferNotificationRepository offerNotificationRepository,
            IOfferNotificationHistoryRepository offerNotificationHistoryRepository,
            ICatalogImportSettingsRepository catalogImportSettingsRepository,
            IMacnaimaService macnaimaService
        )
        {
            _logger = logger;
            _offerNotificationRepository = offerNotificationRepository;
            _offerNotificationHistoryRepository = offerNotificationHistoryRepository;
            _macnaimaService = macnaimaService;
        }

        public async Task Execute(int degreeOfParallelism, CancellationToken cancellationToken)
        {
            degreeOfParallelism = degreeOfParallelism > 0 ? degreeOfParallelism : 1;

            while (
                !cancellationToken.IsCancellationRequested &&
                await _offerNotificationRepository.GetNextPendings(degreeOfParallelism, cancellationToken) is IEnumerable<OfferNotification> offersNotificationPending &&
                offersNotificationPending.Any()
            )
            {
                var tasks = offersNotificationPending
                    .Select(async offerNotificationPending =>
                    {
                        var result = await _macnaimaService.NotifyOffer(offerNotificationPending.Id, cancellationToken);
                        if (result.IsFailure)
                        {
                            _logger.LogWarning($"Failure on notify offer {offerNotificationPending.Id}. Error: {result.Error}");
                            return;
                        }

                        await _offerNotificationRepository.Update(offerNotificationPending.WaitingGetDetail(), cancellationToken);
                        await _offerNotificationHistoryRepository.Add(OfferNotificationHistory.Create(offerNotificationPending), cancellationToken);

                        _logger.LogInformation($"Success on notify offer {offerNotificationPending.Id}");
                    });

                await Task.WhenAny(
                    Task.WhenAll(tasks),
                    Task.Delay(Timeout.Infinite, cancellationToken)
                );
            }
        }
    }
}
