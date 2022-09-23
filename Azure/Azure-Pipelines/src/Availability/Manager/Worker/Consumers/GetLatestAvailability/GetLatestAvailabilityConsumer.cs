using AutoMapper;
using Availability.Manager.Worker.Backend.Application.UseCases.GetLatestAvailability;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using GetLatestAvailabilityUsecase = Availability.Manager.Worker.Backend.Application.UseCases.GetLatestAvailability;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Availability.Manager.Worker.Consumers.GetLatestAvailability
{
    public class GetLatestAvailabilityConsumer : IConsumer<AvailabilityMessagingContracts.Messages.Manager.GetLatestAvailability>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IGetLatestAvailabilityUseCase _getLatestAvailabilityUseCase;

        public GetLatestAvailabilityConsumer(
            IMapper mapper,
            ILogger<GetLatestAvailabilityConsumer> logger,
            IGetLatestAvailabilityUseCase getLatestAvailabilityUseCase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _getLatestAvailabilityUseCase = getLatestAvailabilityUseCase;
        }

        public async Task Consume(ConsumeContext<AvailabilityMessagingContracts.Messages.Manager.GetLatestAvailability> context)
        {
            try
            {
                _logger.LogDebug("Starting consume: {message}", context.Message);
                
                var inbound = _mapper.Map<GetLatestAvailabilityUsecase.Models.Inbound>(context.Message);

                var outbound = await _getLatestAvailabilityUseCase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    _logger.LogWarning("Consumed with failure! Failure: {Error}, {Sku}", outbound.Error, new { inbound.SupplierId, SkuId = inbound.SupplierSkuId });

                    if (context.IsResponseAccepted<MessagingContracts.Shared.Messages.NotFound>())
                    {
                        var notFoundOutput = _mapper.Map<MessagingContracts.Shared.Messages.NotFound>(outbound.Error);
                        await context.RespondAsync(notFoundOutput);
                    }

                    return;
                }

                if (context.IsResponseAccepted<AvailabilityMessagingContracts.Messages.Manager.GetLatestAvailabilityResponse>())
                {
                    var output = _mapper.Map<AvailabilityMessagingContracts.Messages.Manager.GetLatestAvailabilityResponse>(outbound.Value);
                    await context.RespondAsync(output);
                }

                _logger.LogInformation("Successfully consumed! {SupplierSkuId}", new { inbound.SupplierSkuId });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                if (context.IsResponseAccepted<MessagingContracts.Shared.Messages.UnexpectedError>())
                {
                    var unexpectedErrorOutput = _mapper.Map<MessagingContracts.Shared.Messages.UnexpectedError>(error);
                    await context.RespondAsync(unexpectedErrorOutput);
                }
            }
        }
    }
}
