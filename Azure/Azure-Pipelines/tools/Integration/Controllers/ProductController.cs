using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Tools.Integration.Models;
using Tools.Integration.Models.Categorization;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using SharedMessages = Shared.Messaging.Contracts.Shared.Messages;

namespace Catalog.Integration.Tool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IRequestClient<ChangeMessages.SkuMustBeIntegrated> _productMustBeIntegratedClient;
        private readonly IRequestClient<ChangeMessages.GetSkuDetail> _getSkuDetailClient;

        public ProductController(
            IBus bus,
            IRequestClient<ChangeMessages.SkuMustBeIntegrated> productMustBeIntegratedClient,
            IRequestClient<ChangeMessages.GetSkuDetail> getSkuDetailClient
        )
        {
            _bus = bus;
            _productMustBeIntegratedClient = productMustBeIntegratedClient;
            _getSkuDetailClient = getSkuDetailClient;
        }

        [HttpPost("change/skuMustBeIntegrated")]
        public async Task<IActionResult> SkuMustBeIntegrated([FromBody] SkuMustBeIntegratedModel model, CancellationToken cancellationToken)
        {
            var response = await _productMustBeIntegratedClient.GetResponse<
                    ChangeMessages.SkuMustBeIntegratedResponse,
                    SharedMessages.NotFound,
                    SharedMessages.UnexpectedError
                >(model, cancellationToken);

            if (response.Is<ChangeMessages.SkuMustBeIntegratedResponse>(out var successResponse))
                return Ok(successResponse.Message);

            if (response.Is<SharedMessages.NotFound>(out var notFoundResponse))
                return NotFound(notFoundResponse.Message);

            if (response.Is<SharedMessages.UnexpectedError>(out var errorResponse))
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse.Message);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("change/integrateSku")]
        public async Task<IActionResult> IntegrateSku([FromBody] IntegrateSkuModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<ChangeMessages.IntegrateSku>(model, cancellationToken);

            return Accepted();
        }

        [HttpGet("change/getSkuDetail/{skuIntegrationId}")]
        public async Task<IActionResult> GetSkuDetail([FromRoute] GetSkuDetailModel model, CancellationToken cancellationToken)
        {
            var response = await _getSkuDetailClient.GetResponse<
                    ChangeMessages.GetSkuDetailResponse,
                    SharedMessages.NotFound,
                    SharedMessages.UnexpectedError
                >(model, cancellationToken);

            if (response.Is<ChangeMessages.GetSkuDetailResponse>(out var successResponse))
                return Ok(successResponse.Message);

            if (response.Is<SharedMessages.NotFound>(out var notFoundResponse))
                return NotFound(notFoundResponse.Message);

            if (response.Is<SharedMessages.UnexpectedError>(out var errorResponse))
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse.Message);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("saga/enrichment/UpdateSkuEnriched")]
        public async Task<IActionResult> UpdateSkuEnriched([FromBody] UpdateSkuEnrichedModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<SagaMessages.Enrichment.UpdateSkuEnriched>(model, cancellationToken);

            return Accepted();
        }

        [HttpPost("saga/enrichment/sendSkuForEnrichment")]
        public async Task<IActionResult> SendSkuForEnrichment([FromBody] SendSkuForEnrichmentModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<SagaMessages.Enrichment.SendSkuForEnrichment>(model, cancellationToken);

            return Accepted();
        }

        [HttpPost("saga/persistence/upsertSku")]
        public async Task<IActionResult> UpsertSku([FromBody] UpsertSkuModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<SagaMessages.Persistence.UpsertSku>(model, cancellationToken);

            return Accepted();
        }

        [HttpPost("saga/persistence/removeSku")]
        public async Task<IActionResult> RemoveSku([FromBody] RemoveSkuModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<SagaMessages.Persistence.RemoveSku>(model, cancellationToken);

            return Accepted();
        }

        [HttpPost("saga/categorization/categorizeSku")]
        public async Task<IActionResult> CategorizeSku([FromBody] CategorizeSkuModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<SagaMessages.Categorization.CategorizeSku>(model, cancellationToken);

            return Accepted();
        }

        [HttpPost("saga/categorization/skuCategorized")]
        public async Task<IActionResult> SkuCategorized([FromBody] SkuCategorizedModel model, CancellationToken cancellationToken)
        {
            await _bus.Publish<SagaMessages.Categorization.SkuCategorized>(model, cancellationToken);

            return Accepted();
        }
    }
}
