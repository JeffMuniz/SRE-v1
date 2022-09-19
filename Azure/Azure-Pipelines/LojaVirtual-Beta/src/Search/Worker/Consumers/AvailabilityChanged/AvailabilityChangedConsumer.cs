using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using Usecase = Search.Worker.Backend.Application.Usecases.UpdateSkuAvailability;

namespace Search.Worker.Consumers.AvailabilityChanged
{
    public class AvailabilityChangedConsumer : IConsumer<AvailabilityMessagingContracts.Messages.Manager.AvailabilityChanged>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecase.IUpdateSkuAvailabilityUsecase _updateAvailabilityUsecase;

        public AvailabilityChangedConsumer(
            IMapper mapper,
            ILogger<AvailabilityChangedConsumer> logger,
            Usecase.IUpdateSkuAvailabilityUsecase updateAvailabilityUsecase
        )
        {
            _logger = logger;
            _mapper = mapper;
            _updateAvailabilityUsecase = updateAvailabilityUsecase;
        }

        public async Task Consume(ConsumeContext<AvailabilityMessagingContracts.Messages.Manager.AvailabilityChanged> context)
        {
            try
            {
                _logger.LogDebug("Starting to update availability in search index: {sku}", new { context.Message.SupplierId, context.Message.SupplierSkuId, context.Message.MainContract });

                var inbound = _mapper.Map<Usecase.Models.Inbound>(context.Message);

                var outbound = await _updateAvailabilityUsecase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    if (outbound.Error.Code == "IgnoreInput")
                    {
                        _logger.LogDebug("Ignoring update availability {sku}", new { inbound.SupplierId, inbound.SupplierSkuId, inbound.MainContract });
                        return;
                    }

                    _logger.LogWarning("Failure on update availability in search index {sku}. Error: {error}", new { inbound.SupplierId, inbound.SupplierSkuId, inbound.MainContract }, outbound.Error);

                    return;
                }

                _logger.LogInformation("Success on update availability in search index {sku}", new { inbound.SupplierId, inbound.SupplierSkuId, inbound.MainContract });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error update availability in search index {sku}", new { context.Message.SupplierId, context.Message.SupplierSkuId, context.Message.MainContract });

                throw;
            }
        }
    }
}
