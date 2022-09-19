using Integration.Api.Backend.Application.Offer;
using Integration.Api.Backend.Application.Offer.Models;
using Integration.Api.Backend.Application.Offer.UseCases.Create;
using Integration.Api.Backend.Application.Offer.UseCases.GetDetail;
using Integration.Api.Backend.Application.Offer.UseCases.MakeEnrich;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Controllers.Hooks
{
    [ApiController]
    [Route("hooks/offer")]
    [Authorize]
    public class OfferController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IGetDetailUseCase _getDetailUseCase;
        private readonly IMakeEnrichUseCase _makeEnrichUseCase;
        private readonly ICreateUseCase _createUseCase;

        public OfferController(
            ILogger<OfferController> logger,
            IGetDetailUseCase getDetailUseCase,
            IMakeEnrichUseCase makeEnrichUseCase,
            ICreateUseCase createUseCase
        )
        {
            _logger = logger;
            _getDetailUseCase = getDetailUseCase;
            _makeEnrichUseCase = makeEnrichUseCase;
            _createUseCase = createUseCase;
        }

        /// <summary>
        /// Getting offer details
        /// </summary>
        /// <param name="id">Offer id</param>
        /// <returns>Offer details</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        [SwaggerResponse(StatusCodes.Status200OK, "Offer successfully getted", typeof(OfferModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Offer not found", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Offer detail error", typeof(string))]
        public async Task<IActionResult> GetDetail([FromRoute] string id, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting offer {id}.", id);

            var result = await _getDetailUseCase.Execute(id, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToUpper().Contains("NOT FOUND")
                    ? NotFound(result.Error)
                    : BadRequest(result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Notifuy Offer enriched result
        /// </summary>
        /// <param name="id">Offer id</param>
        /// <param name="enrichedOffer">Offer enriched details</param>
        [HttpPost("{id}")]
        [Authorize(Roles = "Admin,User")]
        [SwaggerResponse(StatusCodes.Status200OK, "Offer enriched result successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Offer not found", typeof(string))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Offer enriched result error", typeof(string))]
        public async Task<IActionResult> PostResult([FromRoute] string id, [FromBody] EnrichedOfferModel enrichedOffer, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Enriched offer {id} result.", id);

            var result = await _makeEnrichUseCase.Execute(id, enrichedOffer, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToUpper().Contains("NOT FOUND")
                    ? NotFound(result.Error)
                    : UnprocessableEntity(result.Error);

            return Ok();
        }

        [HttpPost("notification/create")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status201Created, "Offer successfully created")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Offer not found", typeof(string))]
        public async Task<IActionResult> CreateNotification([FromBody] OfferModel offer, CancellationToken cancellationToken)
        {
            var result = await _createUseCase.Execute(offer, cancellationToken);

            if (result.IsFailure)
                return UnprocessableEntity(result.Error);

            return Created($"/offer/{result.Value}", new { Id = result.Value });
        }
    }
}
