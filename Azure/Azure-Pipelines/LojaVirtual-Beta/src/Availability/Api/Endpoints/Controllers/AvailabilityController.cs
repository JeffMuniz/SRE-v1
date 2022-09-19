using AutoMapper;
using Availability.Api.Backend.Infrastructure.Web.Extensions;
using Availability.Api.Endpoints.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Backend.Application.Usecases.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;
using Usecases = Availability.Api.Backend.Application.UseCases;
using SharedModels = Availability.Api.Backend.Application.UseCases.Shared.Models;
using System;

namespace Availability.Api.Endpoints.Controllers
{
    [ApiController]
    [Route("availability")]
    public class AvailabilityController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecases.GetLatestAvailability.IGetLatestAvailabilityUseCase _getLatestAvailabilityUseCase;
        private readonly Usecases.GetPartnerAvailability.IGetPartnerAvailabilityUseCase _getPartnerAvailabilityUseCase;

        public AvailabilityController(
            ILogger<AvailabilityController> logger,
            IMapper mapper,
            Usecases.GetLatestAvailability.IGetLatestAvailabilityUseCase getLatestAvailabilityUseCase,
            Usecases.GetPartnerAvailability.IGetPartnerAvailabilityUseCase getPartnerAvailabilityUseCase
        )
        {
            _logger = logger;
            _mapper = mapper;
            _getLatestAvailabilityUseCase = getLatestAvailabilityUseCase;
            _getPartnerAvailabilityUseCase = getPartnerAvailabilityUseCase;
        }

        /// <summary>
        /// Getting Latest Availability
        /// </summary>
        /// <param name="supplierId">Supplier id</param>
        /// <param name="supplierSkuId">Supplier Sku id</param>
        /// <param name="contractId">Contract id</param>
        /// <param name="persistedSkuId">Persisted Sku id</param>
        /// <param name="shardId">Shard id</param>
        /// <returns>Latest Availability</returns>
        [HttpGet("supplier/{supplierId}/sku/{supplierSkuId}/contract/{contractId}")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Latest Availability successfully getted", typeof(LatestAvailabilityModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Latest Availability error", typeof(Error))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Latest Availability not found", typeof(Error))]
        public async Task<IActionResult> Get(
            [FromRoute] GetLatestAvailabilityIdModel latestAvailabilityId,
            [FromQuery] GetLatestAvailabilityModel latestAvailability,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting latest availability for: {latestAvailabilityId}.", latestAvailabilityId);

            var latestAvailabilityInbound = _mapper.Map<Usecases.GetLatestAvailability.Models.Inbound>(latestAvailability, latestAvailabilityId);

            var latestAvailabilityOutbound = await _getLatestAvailabilityUseCase.Execute(latestAvailabilityInbound, cancellationToken);

            if (latestAvailabilityOutbound.IsFailure)
            {
                if (latestAvailabilityOutbound.Error.Code.NotIs(SharedModels.ErrorBuilder.Codes.RegisterNotFound.ToString()))
                    return StatusCode(latestAvailabilityOutbound.Error.GetHttpStatus(), latestAvailabilityOutbound.Error);

                _logger.LogDebug("Latest availability not found, Getting partner availability for: {latestAvailabilityId}.", latestAvailabilityId);

                var partnerAvailabilityInbound = _mapper.Map<Usecases.GetPartnerAvailability.Models.Inbound>(latestAvailability, latestAvailabilityId);

                var partnerAvailabilityOutbound = await _getPartnerAvailabilityUseCase.Execute(partnerAvailabilityInbound, cancellationToken);

                if (partnerAvailabilityOutbound.IsFailure)
                    return StatusCode(partnerAvailabilityOutbound.Error.GetHttpStatus(), partnerAvailabilityOutbound.Error);

                var partnerAvailabilityResult = _mapper.Map<LatestAvailabilityModel>(partnerAvailabilityOutbound.Value);

                return Ok(partnerAvailabilityResult);
            }

            var latestAvailabilityResult = _mapper.Map<LatestAvailabilityModel>(latestAvailabilityOutbound.Value);

            return Ok(latestAvailabilityResult);
        }

        /// <summary>
        /// Getting Partner Availability
        /// </summary>
        /// <param name="supplierId">Supplier id</param>
        /// <param name="supplierSkuId">Supplier Sku id</param>
        /// <param name="contractId">Contract id</param>
        /// <param name="persistedSkuId">Persisted Sku id</param>
        /// <param name="shardId">Shard id</param>
        /// <returns>Partner Availability</returns>
        [HttpGet("partner/supplier/{supplierId}/sku/{supplierSkuId}/contract/{contractId}")]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "Partner Availability successfully getted", typeof(PartnerAvailabilityModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Partner Availability error", typeof(Error))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Partner Availability not found", typeof(Error))]
        public async Task<IActionResult> GetPartner(
            [FromRoute] GetPartnerAvailabilityIdModel partnerAvailabilityId,
            [FromQuery] GetPartnerAvailabilityModel partnerAvailability,
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting partner availability for: {partnerAvailabilityId}.", partnerAvailabilityId);

            var inbound = _mapper.Map<Usecases.GetPartnerAvailability.Models.Inbound>(partnerAvailability, partnerAvailabilityId);

            var outbound = await _getPartnerAvailabilityUseCase.Execute(inbound, cancellationToken);

            if (outbound.IsFailure)
                return StatusCode(outbound.Error.GetHttpStatus(), outbound.Error);

            var partnerAvailabilityResult = _mapper.Map<PartnerAvailabilityModel>(outbound.Value);

            return Ok(partnerAvailabilityResult);
        }
    }
}
