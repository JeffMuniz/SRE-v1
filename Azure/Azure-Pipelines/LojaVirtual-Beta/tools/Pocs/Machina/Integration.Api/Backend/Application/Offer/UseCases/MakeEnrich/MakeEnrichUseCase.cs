using AutoMapper;
using CSharpFunctionalExtensions;
using Integration.Api.Backend.Application.Offer.Models;
using Integration.Api.Backend.Domain.Entities;
using Integration.Api.Backend.Domain.Repositories;
using Integration.Api.Backend.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.MakeEnrich
{
    public class MakeEnrichUseCase : IMakeEnrichUseCase
    {
        private readonly IMapper _mapper;
        private readonly IOfferNotificationRepository _offerNotificationRepository;
        private readonly IOfferNotificationHistoryRepository _offerNotificationHistoryRepository;

        public MakeEnrichUseCase(
            IMapper mapper,
            IOfferNotificationRepository offerNotificationRepository,
            IOfferNotificationHistoryRepository offerNotificationHistoryRepository
        )
        {
            _mapper = mapper;
            _offerNotificationRepository = offerNotificationRepository;
            _offerNotificationHistoryRepository = offerNotificationHistoryRepository;
        }

        public async Task<Result> Execute(string offerId, EnrichedOfferModel enrichedOffer, CancellationToken cancellationToken)
        {
            if (await _offerNotificationRepository.Get(offerId, cancellationToken) is not OfferNotification offerNotification)
                return Result.Failure("Offer not found");

            var historyBuilder = OfferNotificationHistory.CreateDiff(offerNotification);

            var enrichedOfferReceived = _mapper.Map<EnrichedOffer>(enrichedOffer);

            if (offerNotification.Enriched(enrichedOfferReceived).Status.NotIn(NotificationStatus.AwaitingCompleteEnrichment, NotificationStatus.Enriched))
                return Result.Failure($"Offer status {offerNotification.Status} not accept Enriched");

            await _offerNotificationRepository.Update(offerNotification, cancellationToken);

            var offerNotificationHistory = historyBuilder
                .Bind(offerNotification)
                .Build();

            if (offerNotificationHistory.Changes != null)
                await _offerNotificationHistoryRepository.Add(offerNotificationHistory, cancellationToken);

            return Result.Success();
        }
    }
}
