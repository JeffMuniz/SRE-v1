using MassTransit;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UpsertSkuUsecase = Product.Persistence.Worker.Backend.Application.Usecases.UpsertSku;
using PersistenceMessaging = Shared.Messaging.Contracts.Product.Saga.Messages.Persistence;

namespace Product.Persistence.Worker.Consumers.UpsertSku
{
    public class UpsertSkuConsumer : IConsumer<PersistenceMessaging.UpsertSku>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpsertSkuConsumer> _logger;
        private readonly UpsertSkuUsecase.IUpsertSkuUseCase _upsertSkuUseCase;

        public UpsertSkuConsumer(
            IMapper mapper,
            ILogger<UpsertSkuConsumer> logger,
            UpsertSkuUsecase.IUpsertSkuUseCase upsertSkuUseCase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _upsertSkuUseCase = upsertSkuUseCase;
        }

        public async Task Consume(ConsumeContext<PersistenceMessaging.UpsertSku> context)
        {
            try
            {
                _logger.LogDebug("Starting product persistence notification: {message}", context.Message);

                var inbound = _mapper.Map<UpsertSkuUsecase.Models.Inbound>(context.Message);

                var outbound = await _upsertSkuUseCase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    _logger.LogError("Failure on persist {SupplierSkuId}. Error: {Error}", context.Message.SupplierSku.SkuId, outbound.Error);

                    return;
                }

                var skuUpserted = _mapper.Map<PersistenceMessaging.SkuUpserted>(inbound, outbound.Value);
                await context.Publish(skuUpserted);

                _logger.LogInformation("Success on persist {SupplierSkuId}", context.Message.SupplierSku.SkuId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                throw;
            }
        }
    }
}
