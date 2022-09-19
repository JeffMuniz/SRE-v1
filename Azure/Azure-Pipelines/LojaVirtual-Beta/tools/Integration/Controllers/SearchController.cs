using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tools.Integration.Models.Search;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Catalog.Integration.Tool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IBus _bus;

        public SearchController(
            IBus bus
        )
        {
            _bus = bus;
        }

        [HttpPost("sendSkuToSearchIndex")]
        public async Task<IActionResult> SendSkuToSearchIndex([FromBody] SendSkuToSearchIndexModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<SagaMessages.Search.SendSkuToSearchIndex>(model, cancellationToken);

            return Accepted();
        }

        [HttpPost("skuIndexedInTheSearch")]
        public async Task<IActionResult> SkuIndexedInTheSearch([FromBody] SkuIndexedInTheSearchModel model, CancellationToken cancellationToken)
        {
            Request.Headers.TryGetValue("CorrelationId", out var correlationIdString);

            await _bus.Publish<SagaMessages.Search.SkuIndexedInTheSearch>(
                model,
                ctx => ctx.CorrelationId = Guid.Parse(correlationIdString),
                cancellationToken
            );

            return Accepted();
        }

        [HttpPost("removeSkuFromSearchIndex")]
        public async Task<IActionResult> RemoveSkuFromSearchIndex([FromBody] RemoveSkuFromSearchIndexModel model, CancellationToken cancellationToken)
        {
            await _bus.Send<SagaMessages.Search.RemoveSkuFromSearchIndex>(model, cancellationToken);

            return Accepted();
        }

        [HttpPost("skuRemovedFromSearchIndex")]
        public async Task<IActionResult> SkuRemovedFromSearchIndex([FromBody] SkuRemovedFromSearchIndexModel model, CancellationToken cancellationToken)
        {
            await _bus.Publish<SagaMessages.Search.SkuRemovedFromSearchIndex>(model, cancellationToken);

            return Accepted();
        }        
    }
}
