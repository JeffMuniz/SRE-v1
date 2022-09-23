using AutoMapper;
using CSharpFunctionalExtensions;
using MassTransit;
using Product.Enrichment.Macnaima.Api.Backend.Domain.Entities;
using Product.Enrichment.Macnaima.Api.Backend.Domain.Services;
using Product.Enrichment.Macnaima.Api.Backend.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;
using SharedMessages = Shared.Messaging.Contracts.Shared.Messages;

namespace Product.Enrichment.Macnaima.Api.Backend.Infrastructure.Messaging
{
    public class OfferDetailsService : IOfferDetailsService
    {
        private readonly IMapper _mapper;
        private readonly IRequestClient<ChangeMessages.GetSkuDetail> _getSkuDetailClient;

        public OfferDetailsService(
            IMapper mapper,
            IRequestClient<ChangeMessages.GetSkuDetail> getSkuDetailClient
        )
        {
            _mapper = mapper;
            _getSkuDetailClient = getSkuDetailClient;
        }

        public async Task<Result<Offer>> Get(OfferId offerId, CancellationToken cancellationToken)
        {
            var getSkuDetail = _mapper.Map<ChangeMessages.GetSkuDetail>(offerId);

            var response = await _getSkuDetailClient.GetResponse<
                    ChangeMessages.GetSkuDetailResponse,
                    SharedMessages.NotFound,
                    SharedMessages.UnexpectedError
                >(getSkuDetail, cancellationToken);

            if (response.Is<ChangeMessages.GetSkuDetailResponse>(out var successResponse))
                return _mapper.Map<Offer>(successResponse.Message);

            if (response.Is<SharedMessages.NotFound>(out var notFoundResponse))
                return Result.Failure<Offer>(notFoundResponse.Message.Message);

            if (response.Is<SharedMessages.UnexpectedError>(out var unexpectedErrorResponse))
                return Result.Failure<Offer>(unexpectedErrorResponse.Message.Message);

            return Result.Failure<Offer>("The requested offer failed");
        }
    }
}
