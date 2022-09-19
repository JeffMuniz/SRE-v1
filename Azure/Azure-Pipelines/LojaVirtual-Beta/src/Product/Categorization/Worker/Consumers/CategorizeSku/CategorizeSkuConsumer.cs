using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using CategorizationMessaging = Shared.Messaging.Contracts.Product.Saga.Messages.Categorization;
using CategorizeSkuUsecase = Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku;

namespace Product.Categorization.Worker.Consumers.CategorizeSku
{
    public class CategorizeSkuConsumer : IConsumer<CategorizationMessaging.CategorizeSku>
    {        
        private readonly IMapper _mapper;
        private readonly ILogger<CategorizeSkuConsumer> _logger;
        private readonly CategorizeSkuUsecase.ICategorizeSkuUsecase _categorizeSkuUsecase;

        public CategorizeSkuConsumer(
            IMapper mapper,
            ILogger<CategorizeSkuConsumer> logger,
            CategorizeSkuUsecase.ICategorizeSkuUsecase categorizeSkuUsecase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _categorizeSkuUsecase = categorizeSkuUsecase;
        }

        public async Task Consume(ConsumeContext<CategorizationMessaging.CategorizeSku> context)
        {
            try
            {
                _logger.LogDebug("Starting sku categorize: {message}", context.Message);

                var inbound = _mapper.Map<CategorizeSkuUsecase.Models.Inbound>(context.Message);

                var outbound = await _categorizeSkuUsecase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    _logger.LogError("Failure on categorize sku {SupplierId} - {SupplierSkuId}. Error: {Error}", context.Message.SupplierSku.SupplierId, context.Message.SupplierSku.SkuId, outbound.Error);

                    return;
                }

                var skuCategorized = _mapper.Map<CategorizationMessaging.SkuCategorized>(inbound, outbound.Value);
                await context.Publish(skuCategorized);

                _logger.LogInformation("Success on categorize sku {SupplierId} - {SupplierSkuId}", context.Message.SupplierSku.SupplierId, context.Message.SupplierSku.SkuId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                throw;
            }
        }
    }
}
