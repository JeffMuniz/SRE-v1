using AutoMapper;
using CSharpFunctionalExtensions;
using Product.Enrichment.Macnaima.Api.Backend.Domain.Services;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Enrichment.Macnaima.Api.Backend.Application.Usecases.MakeEnrich
{
    public class MakeEnrichUsecase : IMakeEnrichUsecase
    {
        private readonly IMapper _mapper;
        private readonly IEnrichedOfferService _enrichedService;

        public MakeEnrichUsecase(
            IMapper mapper,
            IEnrichedOfferService enrichedService
        )
        {
            _mapper = mapper;
            _enrichedService = enrichedService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var enrichedOffer = _mapper.Map<Domain.Entities.EnrichedOffer>(inbound);

            var notifyUpdateEnrichedResult = await _enrichedService.NotifyUpdate(enrichedOffer, cancellationToken);
            if (notifyUpdateEnrichedResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(notifyUpdateEnrichedResult);

            return Models.Outbound.Create();
        }
    }
}
