using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Enrichment.macnaima.Api.Endpoints.Models;
using Product.Enrichment.macnaima.Api.Infrastructure.Web.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;
using Usecases = Product.Enrichment.macnaima.Api.Backend.Application.Usecases;

namespace Product.Enrichment.macnaima.Api.Endpoints.Controllers
{
    [ApiController]
    [Route("hooks")]
    [Authorize]
    public class OfferDetailsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecases.GetOfferDetails.IGetOfferDetailsUsecases _getOfferDetailsUseCase;

        public OfferDetailsController(
            ILogger<OfferDetailsController> logger,
            IMapper mapper,
            Usecases.GetOfferDetails.IGetOfferDetailsUsecases getOfferDetailsUseCase
        )
        {
            _logger = logger;
            _mapper = mapper;
            _getOfferDetailsUseCase = getOfferDetailsUseCase;
        }

        /// <summary>
        /// Getting offer details
        /// </summary>
        /// <param name="id">Offer id</param>
        /// <returns>Offer details</returns>
        [HttpGet("offer/{id}")]
        [Authorize(Roles = "Admin,User")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Offer successfully getted", typeof(OfferModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Offer detail error", typeof(Error))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Offer not found", typeof(Error))]
        public async Task<IActionResult> Get([FromRoute] OfferIdModel detailsOfferId, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting details for offer {id}.", detailsOfferId.OfferId);

            var inbound = _mapper.Map<Usecases.GetOfferDetails.Models.Inbound>(detailsOfferId);

            var outbound = await _getOfferDetailsUseCase.Execute(inbound, cancellationToken);

            if (outbound.IsFailure)
                return StatusCode(outbound.Error.GetHttpStatus(), outbound.Error);

            var offerModel = _mapper.Map<OfferModel>(outbound.Value);

            return Ok(offerModel);
        }
    }
}
