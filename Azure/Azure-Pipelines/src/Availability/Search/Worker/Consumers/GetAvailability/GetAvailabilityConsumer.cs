using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;
using GetAvailabilityUsecase = Availability.Search.Worker.Backend.Application.UseCases.GetAvailability;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Availability.Search.Worker.Consumers.GetAvailability
{
    public class GetAvailabilityConsumer : IConsumer<AvailabilityMessaging.Search.GetAvailability>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly GetAvailabilityUsecase.IGetAvailabilityUseCase _getAvailabilityUseCase;

        public GetAvailabilityConsumer(
            IMapper mapper,
            ILogger<GetAvailabilityConsumer> logger,
            GetAvailabilityUsecase.IGetAvailabilityUseCase getAvailabilityUseCase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _getAvailabilityUseCase = getAvailabilityUseCase;
        }

        public async Task Consume(ConsumeContext<AvailabilityMessaging.Search.GetAvailability> context)
        {
            try
            {
                _logger.LogDebug("Starting get availability notification: {message}", context.Message);
                
                var inbound = _mapper.Map<GetAvailabilityUsecase.Models.Inbound>(context.Message);

                var outbound = await _getAvailabilityUseCase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    _logger.LogWarning("Failure on get {SupplierSkuId}. Error: {Error}", context.Message.SupplierSkuId, outbound.Error);

                    if (context.IsResponseAccepted<MessagingContracts.Shared.Messages.NotFound>())
                    {
                        var notFoundOutput = _mapper.Map<MessagingContracts.Shared.Messages.NotFound>(outbound.Error);
                        await context.RespondAsync(notFoundOutput);
                    }

                    return;
                }

                var availabilityFound = _mapper.Map<AvailabilityMessaging.Search.AvailabilityFound>(context.Message, outbound.Value);

                await context.Publish(availabilityFound);

                if (context.IsResponseAccepted<AvailabilityMessaging.Search.AvailabilityFound>())
                    await context.RespondAsync(availabilityFound);

                _logger.LogInformation("Success on get availability {SupplierSkuId}", context.Message.SupplierSkuId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                if (context.IsResponseAccepted<MessagingContracts.Shared.Messages.UnexpectedError>())
                {
                    var unexpectedErrorOutput = _mapper.Map<MessagingContracts.Shared.Messages.UnexpectedError>(error);
                    await context.RespondAsync(unexpectedErrorOutput);
                }
                else
                    throw;
            }
        }

    }
}
