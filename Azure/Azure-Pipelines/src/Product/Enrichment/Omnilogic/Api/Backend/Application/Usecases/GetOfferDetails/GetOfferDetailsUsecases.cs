using AutoMapper;
using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;
using Product.Enrichment.Macnaima.Api.Backend.Application.Usecases.Shared.Models;

namespace Product.Enrichment.Macnaima.Api.Backend.Application.Usecases.GetOfferDetails
{
    public class GetOfferDetailsUsecases : IGetOfferDetailsUsecases
    {
        private readonly IMapper _mapper;
        private readonly Domain.Services.IOfferDetailsService _skuDetailsService;

        public GetOfferDetailsUsecases(IMapper mapper, Domain.Services.IOfferDetailsService skuDetailsService)
        {
            _mapper = mapper;
            _skuDetailsService = skuDetailsService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(inbound.OfferId))
                return ErrorBuilder.CreateInvalidBusinessRule($"The {nameof(inbound.OfferId)} must be provided");

            var offerId = _mapper.Map<Domain.ValueObjects.OfferId>(inbound);
            var skuDetailsResult = await _skuDetailsService.Get(offerId, cancellationToken);

            if (skuDetailsResult.IsFailure)
                return ErrorBuilder.CreateRegisterNotFound(skuDetailsResult.Error);

            var offerModel = _mapper.Map<Models.Outbound>(skuDetailsResult.Value);
            return offerModel;
        }
    }
}
