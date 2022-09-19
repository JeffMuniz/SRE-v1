using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using RemoveSkuUsecase = Availability.Manager.Worker.Backend.Application.UseCases.RemoveSku;

namespace Availability.Manager.Worker.Consumers.RemoveSku
{
    public class RemoveSkuConsumer : IConsumer<AvailabilityMessagingContracts.Messages.Manager.RemoveSku>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly RemoveSkuUsecase.IRemoveSkuUseCase _removeSkuUsecase;

        public RemoveSkuConsumer(
            IMapper mapper,
            ILogger<RemoveSkuConsumer> logger,
            RemoveSkuUsecase.IRemoveSkuUseCase removeSkuUsecase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _removeSkuUsecase = removeSkuUsecase;
        }

        public async Task Consume(ConsumeContext<AvailabilityMessagingContracts.Messages.Manager.RemoveSku> context)
        {
            try
            {
                _logger.LogDebug("Starting remove sku notification: {message}", context.Message);

                var inbound = _mapper.Map<RemoveSkuUsecase.Models.Inbound>(context.Message);

                var outbound = await _removeSkuUsecase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure && outbound.Error.Code != "NotFound")
                {
                    _logger.LogWarning("Failure on get {SupplierSkuId}. Error: {Error}", context.Message.SupplierSkuId, outbound.Error);

                    return;
                }

                var skuRemovedFromAvailabilityMessage = _mapper.Map<AvailabilityMessagingContracts.Messages.Manager.SkuRemovedFromAvailability>(context.Message);
                await context.Publish(skuRemovedFromAvailabilityMessage);

                _logger.LogInformation("Success on remove sku {SupplierSkuId}", context.Message.SupplierSkuId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                throw;
            }
        }
    }
}
