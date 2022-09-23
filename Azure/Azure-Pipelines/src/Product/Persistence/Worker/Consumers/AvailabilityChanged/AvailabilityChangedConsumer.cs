using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Product.Persistence.Worker.Backend.Application.Usecases.UpdateAvailability;
using Product.Persistence.Worker.Backend.Application.Usecases.UpdateAvailability.Models;
using System;
using System.Threading.Tasks;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;

namespace Product.Persistence.Worker.Consumers.AvailabilityChanged
{
    public class AvailabilityChangedConsumer : IConsumer<AvailabilityMessaging.Manager.AvailabilityChanged>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUpdateAvailabilityUsecase _updateAvailabilityUsecase;

        public AvailabilityChangedConsumer(
            IMapper mapper,
            ILogger<AvailabilityChangedConsumer> logger,
            IUpdateAvailabilityUsecase updateAvailabilityUsecase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _updateAvailabilityUsecase = updateAvailabilityUsecase;
        }

        public async Task Consume(ConsumeContext<AvailabilityMessaging.Manager.AvailabilityChanged> context)
        {
            try
            {
                _logger.LogDebug("Starting update availability {sku}", new { context.Message.SupplierId, context.Message.SupplierSkuId, context.Message.MainContract });

                var inbound = _mapper.Map<Inbound>(context.Message);

                var outbound = await _updateAvailabilityUsecase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    if (outbound.Error.Code == "IgnoreInput")
                    {
                        _logger.LogDebug("Ignoring update availability {sku}", new { inbound.SupplierId, inbound.SupplierSkuId, inbound.MainContract });
                        return;
                    }

                    _logger.LogError("Failure on update availability {sku}. Error: {error}", new { inbound.SupplierId, inbound.SupplierSkuId, inbound.MainContract }, outbound.Error);

                    return;
                }

                _logger.LogInformation("Success on update availability {sku}", new { inbound.SupplierId, inbound.SupplierSkuId, inbound.MainContract });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error update availability {sku}", new { context.Message.SupplierId, context.Message.SupplierSkuId, context.Message.MainContract });

                throw;
            }
        }
    }
}
