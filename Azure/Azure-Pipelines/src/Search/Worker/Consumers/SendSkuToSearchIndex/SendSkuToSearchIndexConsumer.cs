using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using Usecase = Search.Worker.Backend.Application.Usecases.UpsertSku;

namespace Search.Worker.Consumers.SendSkuToSearchIndex
{
    public class SendSkuToSearchIndexConsumer : IConsumer<SagaMessages.Search.SendSkuToSearchIndex>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecase.IUpsertSkuUsecase _upsertSkuUsecase;

        public SendSkuToSearchIndexConsumer(
            ILogger<SendSkuToSearchIndexConsumer> logger,
            IMapper mapper,
            Usecase.IUpsertSkuUsecase upsertSkuUsecase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _upsertSkuUsecase = upsertSkuUsecase;
        }

        public async Task Consume(ConsumeContext<SagaMessages.Search.SendSkuToSearchIndex> context)
        {
            try
            {
                _logger.LogDebug("Starting to upsert sku into search index: {sku}", new { context.Message.SupplierId, context.Message.SupplierSkuId });

                var inbound = _mapper.Map<Usecase.Models.Inbound>(context.Message);

                var outbound = await _upsertSkuUsecase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    _logger.LogWarning("Failure on upsert sku into search index {sku}. Error: {error}", new { inbound.SupplierId, inbound.SupplierSkuId }, outbound.Error);

                    return;
                }

                var skuIndexedInTheSearchMessage = _mapper.Map<SagaMessages.Search.SkuIndexedInTheSearch>(outbound.Value);
                await context.Publish(skuIndexedInTheSearchMessage);

                _logger.LogInformation("Success on upsert sku into search index {sku}", new { inbound.SupplierId, inbound.SupplierSkuId });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                throw;
            }
        }
    }
}
