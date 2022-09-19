using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Tools.Integration.Models.Availability;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;
using SharedMessages = Shared.Messaging.Contracts.Shared.Messages;

namespace Catalog.Integration.Tool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IRequestClient<AvailabilityMessages.Manager.GetUnavailableSkus> _getUnavailableSkusClient;
        private readonly IRequestClient<AvailabilityMessages.Manager.GetLatestAvailability> _getLatestAvailabilityClient;

        public AvailabilityController(
            IBus bus,
            IRequestClient<AvailabilityMessages.Manager.GetUnavailableSkus> getUnavailableSkusClient,
            IRequestClient<AvailabilityMessages.Manager.GetLatestAvailability> getLatestAvailabilityClient
        )
        {
            _bus = bus;
            _getUnavailableSkusClient = getUnavailableSkusClient;
            _getLatestAvailabilityClient = getLatestAvailabilityClient;
        }

        [HttpGet("search/getAvailability")]
        public async Task<IActionResult> GetAvailability([FromBody] GetAvailabilityModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<AvailabilityMessages.Search.GetAvailability>(model, cancellationToken);

            return Accepted();
        }

        [HttpGet("search/getAvailabilityForAllContracts")]
        public async Task<IActionResult> GetAvailabilityForAllContracts([FromBody] GetAvailabilityForAllContractsModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<AvailabilityMessages.Search.GetAvailabilityForAllContracts>(model, cancellationToken);

            return Accepted();
        }

        [HttpGet("manager/removeSku")]
        public async Task<IActionResult> RemoveSku([FromBody] RemoveSkuModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<AvailabilityMessages.Manager.RemoveSku>(model, cancellationToken);

            return Accepted();
        }

        [HttpGet("manager/getUnavailableSkus")]
        public async Task<IActionResult> GetUnavailableSkus([FromBody] GetUnavailableSkusModel model, CancellationToken cancellationToken)
        {
            var response = await _getUnavailableSkusClient.GetResponse<
                    AvailabilityMessages.Manager.UnavailableSkusResponse,
                    SharedMessages.UnexpectedError
                >(model, cancellationToken);

            if (response.Is<AvailabilityMessages.Manager.UnavailableSkusResponse>(out var successResponse))
                return Ok(successResponse.Message);

            if (response.Is<SharedMessages.UnexpectedError>(out var errorResponse))
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse.Message);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("manager/getLatestAvailability")]
        public async Task<IActionResult> GetLatestAvailability([FromBody] GetLatestAvailabilityModel model, CancellationToken cancellationToken)
        {
            var response = await _getLatestAvailabilityClient.GetResponse<
                    AvailabilityMessages.Manager.GetLatestAvailabilityResponse,
                    SharedMessages.NotFound,
                    SharedMessages.UnexpectedError
                >(model, cancellationToken);

            if (response.Is<AvailabilityMessages.Manager.GetLatestAvailabilityResponse>(out var successResponse))
                return Ok(successResponse.Message);

            if (response.Is<SharedMessages.NotFound>(out var notFoundResponse))
                return NotFound(notFoundResponse.Message);

            if (response.Is<SharedMessages.UnexpectedError>(out var errorResponse))
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse.Message);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("availabilityChanged")]
        public async Task<IActionResult> AvailabilityChanged([FromBody] AvailabilityChangedModel model, CancellationToken cancellationToken)
        {
            await _bus.Publish<AvailabilityMessages.Manager.AvailabilityChanged>(model, cancellationToken);

            return Accepted();
        }

        [HttpGet("manager/checkAvailabilityCacheMustBeRenewed")]
        public async Task<IActionResult> CheckAvailabilityCacheMustBeRenewed([FromBody] CheckAvailabilityCacheMustBeRenewedModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<AvailabilityMessages.Manager.CheckAvailabilityCacheMustBeRenewed>(model, cancellationToken);

            return Accepted();
        }
    }
}
