using AutoMapper;
using CSharpFunctionalExtensions;
using Integration.Api.Backend.Application.Offer.Models;
using Integration.Api.Backend.Domain.Entities;
using Integration.Api.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.Create
{
    public class CreateUseCase : ICreateUseCase
    {
        private readonly IMapper _mapper;
        private readonly IOfferNotificationRepository _offerNotificationRepository;

        public CreateUseCase(
            IMapper mapper,
            IOfferNotificationRepository offerNotificationRepository
        )
        {
            _mapper = mapper;
            _offerNotificationRepository = offerNotificationRepository;
        }

        public async Task<Result<string>> Execute(OfferModel offer, CancellationToken cancellationToken)
        {
            var offerNotification = _mapper.Map<OfferNotification>(offer);

            await _offerNotificationRepository.Add(offerNotification, cancellationToken);

            return offerNotification.Id;
        }
    }
}
