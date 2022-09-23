using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Usecase = Search.Worker.Backend.Application.Usecases.RemoveSku;
using SearchMessages = Shared.Messaging.Contracts.Product.Saga.Messages.Search;

namespace Search.Worker.Consumers.RemoveSkuFromSearchIndex
{
    public class RemoveSkuFromSearchIndexConsumer : IConsumer<SearchMessages.RemoveSkuFromSearchIndex>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecase.IRemoveSkuUsecase _removeSkuFromSearchIndex;

        public RemoveSkuFromSearchIndexConsumer(
            ILogger<RemoveSkuFromSearchIndexConsumer> logger,
            IMapper mapper,
            Usecase.IRemoveSkuUsecase removeSkuFromSearchIndex
        )
        {
            _logger = logger;
            _mapper = mapper;
            _removeSkuFromSearchIndex = removeSkuFromSearchIndex;
        }

        public async Task Consume(ConsumeContext<SearchMessages.RemoveSkuFromSearchIndex> context)
        {
            try
            {
                _logger.LogDebug("Starting to remove sku from search index: {sku}", new { context.Message.SupplierId, context.Message.SupplierSkuId } );

                var inbound = _mapper.Map<Usecase.Models.Inbound>(context.Message);

                var outbound = await _removeSkuFromSearchIndex.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure && outbound.Error.Code != "NotFound")
                {
                    _logger.LogWarning("Failure on remove sku from search index {sku}. Error: {error}", new { inbound.SupplierId, inbound.SupplierSkuId }, outbound.Error);

                    return;
                }

                var skuRemovedFromSearchIndexMessage = _mapper.Map<SearchMessages.SkuRemovedFromSearchIndex>(inbound);
                await context.Publish(skuRemovedFromSearchIndexMessage);

                _logger.LogInformation("Success on remove sku from search index {sku}", new { inbound.SupplierId, inbound.SupplierSkuId });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                throw;
            }
        }
    }
}
