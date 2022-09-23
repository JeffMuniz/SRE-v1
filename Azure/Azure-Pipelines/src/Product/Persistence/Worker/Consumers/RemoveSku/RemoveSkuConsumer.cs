using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Product.Persistence.Worker.Backend.Application.Usecases.RemoveSku;
using Product.Persistence.Worker.Backend.Application.Usecases.RemoveSku.Models;
using System;
using System.Threading.Tasks;
using PersistenceMessaging = Shared.Messaging.Contracts.Product.Saga.Messages.Persistence;

namespace Product.Persistence.Worker.Consumers.RemoveSku
{
    public class RemoveSkuConsumer : IConsumer<PersistenceMessaging.RemoveSku>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IRemoveSkuUsecase _removeSkuUsecase;

        public RemoveSkuConsumer(
            IMapper mapper,
            ILogger<RemoveSkuConsumer> logger,
            IRemoveSkuUsecase removeSkuUsecase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _removeSkuUsecase = removeSkuUsecase;
        }

        public async Task Consume(ConsumeContext<PersistenceMessaging.RemoveSku> context)
        {
            try
            {
                _logger.LogDebug("Starting remove sku: {message}", context.Message);

                var inbound = _mapper.Map<Inbound>(context.Message);

                var outbound = await _removeSkuUsecase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    _logger.LogError("Failure on remove sku SupplierId: {SupplierId}, SupplierSkuId: {SupplierSkuId}. Error: {Error}", context.Message.SupplierId, context.Message.SupplierSkuId, outbound.Error);

                    return;
                }

                var skuRemoved = _mapper.Map<PersistenceMessaging.SkuRemoved>(inbound);
                await context.Publish(skuRemoved);

                _logger.LogInformation("Success on remove sku SupplierId: {SupplierId}, SupplierSkuId: {SupplierSkuId}", context.Message.SupplierId, context.Message.SupplierSkuId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error on remove sku SupplierId: {SupplierId}, SupplierSkuId: {SupplierSkuId}", context.Message.SupplierId, context.Message.SupplierSkuId);

                throw;
            }
        }
    }
}
