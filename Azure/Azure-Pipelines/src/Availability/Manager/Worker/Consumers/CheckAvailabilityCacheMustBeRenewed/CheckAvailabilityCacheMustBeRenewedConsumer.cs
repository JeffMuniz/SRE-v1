using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using CheckAvailabilityCacheUsecase = Availability.Manager.Worker.Backend.Application.UseCases.CheckAvailabilityCacheMustBeRenewed;

namespace Availability.Manager.Worker.Consumers.CheckAvailabilityCacheMustBeRenewed
{
    public class CheckAvailabilityCacheMustBeRenewedConsumer : IConsumer<AvailabilityMessagingContracts.Messages.Manager.CheckAvailabilityCacheMustBeRenewed>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly CheckAvailabilityCacheUsecase.ICheckAvailabilityCacheMustBeRenewedUseCase _checkAvailabilityCacheUsecase;

        public CheckAvailabilityCacheMustBeRenewedConsumer(
            ILogger<CheckAvailabilityCacheMustBeRenewedConsumer> logger,
            IMapper mapper,
            CheckAvailabilityCacheUsecase.ICheckAvailabilityCacheMustBeRenewedUseCase checkAvailabilityCacheUsecase
        )
        {
            _logger = logger;
            _mapper = mapper;
            _checkAvailabilityCacheUsecase = checkAvailabilityCacheUsecase;
        }

        public async Task Consume(ConsumeContext<AvailabilityMessagingContracts.Messages.Manager.CheckAvailabilityCacheMustBeRenewed> context)
        {
            try
            {
                _logger.LogDebug("Starting check availability cache notification: {message}", context.Message);

                var inbound = _mapper.Map<CheckAvailabilityCacheUsecase.Models.Inbound>(context.Message);

                var outbound = await _checkAvailabilityCacheUsecase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    _logger.LogWarning("Failure on get {SupplierSkuId}. Error: {Error}", context.Message.SupplierSkuId, outbound.Error);

                    return;
                }

                _logger.LogInformation("Success on check availability cache {SupplierSkuId}", context.Message.SupplierSkuId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                throw;
            }
        }
    }
}
