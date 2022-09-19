using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;
using UseCases = Availability.Search.Worker.Backend.Application.UseCases.GetAvailabilityForAllContracts;
using AvailabilityShared = Availability.Search.Worker.Backend.Application.UseCases.Shared;

namespace Availability.Search.Worker.Consumers.GetAvailabilityForAllContracts
{
    public class GetAvailabilityForAllContractsConsumer : IConsumer<AvailabilityMessaging.Search.GetAvailabilityForAllContracts>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly UseCases.IGetAvailabilityForAllContractsUseCase _getAvailabilityForAllContractsUseCase;

        public GetAvailabilityForAllContractsConsumer(
            IMapper mapper,
            ILogger<GetAvailabilityForAllContractsConsumer> logger,
            UseCases.IGetAvailabilityForAllContractsUseCase getAvailabilityForAllContractsUseCase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _getAvailabilityForAllContractsUseCase = getAvailabilityForAllContractsUseCase;
        }

        public async Task Consume(ConsumeContext<AvailabilityMessaging.Search.GetAvailabilityForAllContracts> context)
        {
            try
            {
                _logger.LogDebug("Starting get availability for all contracts notification: {message}", context.Message);

                var inbound = _mapper.Map<UseCases.Models.Inbound>(context.Message);

                var result = await _getAvailabilityForAllContractsUseCase.Execute(inbound, context.CancellationToken);
                if (result.IsFailure)
                {
                    _logger.LogWarning("Failure on get {SupplierSkuId}. Error: {Error}", context.Message.SupplierSkuId, result.Error);

                    return;
                }

                foreach(var availabilityOutbound in result.Value)
                {
                    var availabilityFound = _mapper.Map<AvailabilityMessaging.Search.AvailabilityFound>(context.Message, availabilityOutbound);

                    await context.Publish(availabilityFound);
                }

                _logger.LogInformation("Success on get availability for all contracts {SupplierSkuId}", context.Message.SupplierSkuId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                throw;
            }
        }
    }
}
