using AutoMapper;
using CSharpFunctionalExtensions;
using MassTransit;
using Product.Enrichment.Macnaima.Api.Backend.Domain.Services;
using Shared.Messaging.Configuration;
using System.Threading;
using System.Threading.Tasks;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Product.Enrichment.Macnaima.Api.Backend.Infrastructure.Messaging
{
    public class EnrichedOfferService : IEnrichedOfferService
    {
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public EnrichedOfferService(
            IMapper mapper,
            ISendEndpointProvider sendEndpointProvider
        )
        {
            _mapper = mapper;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<Result> NotifyUpdate(Domain.Entities.EnrichedOffer enrichedOffer, CancellationToken cancellationToken)
        {
            var message = _mapper.Map<SagaMessages.Enrichment.UpdateSkuEnriched>(enrichedOffer);

            await _sendEndpointProvider.Send(message, cancellationToken);

            return Result.Success();
        }
    }
}
