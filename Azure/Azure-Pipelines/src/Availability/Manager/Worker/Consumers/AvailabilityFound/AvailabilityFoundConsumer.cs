using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AvailabilityFoundUsecase = Availability.Manager.Worker.Backend.Application.UseCases.AvailabilityFound;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Manager.Worker.Consumers.AvailabilityFound
{
    public class AvailabilityFoundConsumer : IConsumer<AvailabilityMessaging.Search.AvailabilityFound>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly AvailabilityFoundUsecase.IAvailabilityFoundUseCase _availabilityFoundUsecase;

        public AvailabilityFoundConsumer(
            IMapper mapper,
            ILogger<AvailabilityFoundConsumer> logger,
            AvailabilityFoundUsecase.IAvailabilityFoundUseCase availabilityFoundUsecase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _availabilityFoundUsecase = availabilityFoundUsecase;
        }

        public async Task Consume(ConsumeContext<AvailabilityMessaging.Search.AvailabilityFound> context)
        {
            try
            {
                _logger.LogDebug("Starting availability found notification: {message}", context.Message);

                var inbound = _mapper.Map<AvailabilityFoundUsecase.Models.Inbound>(context, context.Message);

                var outbound = await _availabilityFoundUsecase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    _logger.LogWarning("Failure on get {SupplierSkuId}. Error: {Error}", context.Message.SupplierSkuId, outbound.Error);

                    return;
                }

                _logger.LogInformation("Success on availability found {SupplierSkuId}", context.Message.SupplierSkuId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                throw;
            }
        }
    }
}
