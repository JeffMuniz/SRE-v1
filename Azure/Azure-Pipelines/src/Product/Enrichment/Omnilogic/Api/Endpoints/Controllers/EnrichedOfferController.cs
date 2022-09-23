using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Enrichment.Macnaima.Api.Endpoints.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;
using Usecases = Product.Enrichment.Macnaima.Api.Backend.Application.Usecases;

namespace Product.Enrichment.Macnaima.Api.Endpoints.Controllers
{
    [ApiController]
    [Route("hooks")]
    [Authorize]
    public class EnrichedOfferController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecases.MakeEnrich.IMakeEnrichUsecase _makeEnrichUseCase;

        public EnrichedOfferController(
            ILogger<EnrichedOfferController> logger,
            IMapper mapper,
            Usecases.MakeEnrich.IMakeEnrichUsecase makeEnrichUseCase
        )
        {
            _logger = logger;
            _mapper = mapper;
            _makeEnrichUseCase = makeEnrichUseCase;
        }

        /// <summary>
        /// Notify Offer enriched result
        /// </summary>
        /// <param name="id">Offer id</param>
        /// <param name="enrichedOffer">Offer enriched details</param>
        [HttpPost("offer/{id}")]
        [Authorize(Roles = "Admin,User")]
        [Consumes("application/json")]
        [SwaggerResponse(StatusCodes.Status202Accepted, "Offer enriched successfully received")]
        public async Task<IActionResult> Post(
            [FromRoute] OfferIdModel enrichedOfferId,
            [FromBody] EnrichedOfferModel enrichedOffer,
            CancellationToken cancellationToken
        )
        {
            _logger.LogDebug("Enriched offer {id} result.", enrichedOfferId.OfferId);

            var inbound = _mapper.Map<Usecases.MakeEnrich.Models.Inbound>(enrichedOfferId, enrichedOffer);

            var outbound = await _makeEnrichUseCase.Execute(inbound, cancellationToken);

            if (outbound.IsFailure)
                return UnprocessableEntity(outbound.Error);

            return Accepted();
        }
    }
}
