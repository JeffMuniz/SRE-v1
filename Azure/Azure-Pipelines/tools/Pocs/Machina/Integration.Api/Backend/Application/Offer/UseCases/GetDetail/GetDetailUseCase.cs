using AutoMapper;
using CSharpFunctionalExtensions;
using Integration.Api.Backend.Application.Offer.Models;
using Integration.Api.Backend.Domain.Entities;
using Integration.Api.Backend.Domain.Repositories;
using Integration.Api.Backend.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.GetDetail
{
    public class GetDetailUseCase : IGetDetailUseCase
    {
        private readonly IMapper _mapper;
        private readonly IOfferNotificationRepository _offerNotificationRepository;
        private readonly IOfferNotificationHistoryRepository _offerNotificationHistoryRepository;

        public GetDetailUseCase(
            IMapper mapper,
            IOfferNotificationRepository offerNotificationRepository,
            IOfferNotificationHistoryRepository offerNotificationHistoryRepository
        )
        {
            _mapper = mapper;
            _offerNotificationRepository = offerNotificationRepository;
            _offerNotificationHistoryRepository = offerNotificationHistoryRepository;
        }

        public async Task<Result<OfferModel>> Execute(string offerId, CancellationToken cancellationToken)
        {
            if (await _offerNotificationRepository.Get(offerId, cancellationToken) is not OfferNotification offerNotification)
                return Result.Failure<OfferModel>("Offer not found");

            var offerDetail = _mapper.Map<OfferModel>(offerNotification);

            if (
                offerNotification.Status.Is(NotificationStatus.AwaitingEnrichment) ||
                offerNotification.AwaitingEnrichment().Status.IsNot(NotificationStatus.AwaitingEnrichment)
            )
                return offerDetail;

            await _offerNotificationRepository.Update(offerNotification, cancellationToken);

            await _offerNotificationHistoryRepository.Add(OfferNotificationHistory.Create(offerNotification), cancellationToken);

            return offerDetail;
        }
    }
}
