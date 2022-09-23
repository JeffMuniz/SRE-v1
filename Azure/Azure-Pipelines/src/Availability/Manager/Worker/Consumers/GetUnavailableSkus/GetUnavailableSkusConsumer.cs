using AutoMapper;
using Availability.Manager.Worker.Backend.Application.UseCases.GetUnavailableSkus;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using GetUnavailableSkusUsecase = Availability.Manager.Worker.Backend.Application.UseCases.GetUnavailableSkus;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Availability.Manager.Worker.Consumers.GetUnavailableSkus
{
    public class GetUnavailableSkusConsumer : IConsumer<AvailabilityMessagingContracts.Messages.Manager.GetUnavailableSkus>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IGetUnavailableSkusUseCase _getUnavailableSkusUsecase;

        public GetUnavailableSkusConsumer(
            IMapper mapper,
            ILogger<GetUnavailableSkusConsumer> logger,
            IGetUnavailableSkusUseCase getUnavailableSkusUsecase
        )
        {
            _mapper = mapper;
            _logger = logger;
            _getUnavailableSkusUsecase = getUnavailableSkusUsecase;
        }

        public async Task Consume(ConsumeContext<AvailabilityMessagingContracts.Messages.Manager.GetUnavailableSkus> context)
        {
            try
            {
                _logger.LogDebug("Starting get sku main contract unavailable notification: {message}", context.Message);

                var inbound = _mapper.Map<GetUnavailableSkusUsecase.Models.Inbound>(context.Message);

                var outbound = await _getUnavailableSkusUsecase.Execute(inbound, context.CancellationToken);
                if (outbound.IsFailure)
                {
                    _logger.LogWarning("Consumed with failure! Failure: {Error}, Index: {PageIndex}, Size: {PageSize}", outbound.Error, inbound.PageIndex, inbound.PageSize);

                    if (context.IsResponseAccepted<MessagingContracts.Shared.Messages.UnexpectedError>())
                    {
                        var unexpectedErrorOutput = _mapper.Map<MessagingContracts.Shared.Messages.UnexpectedError>(outbound.Error);
                        await context.RespondAsync(unexpectedErrorOutput);
                    }

                    return;
                }

                if (context.IsResponseAccepted<AvailabilityMessagingContracts.Messages.Manager.UnavailableSkusResponse>())
                {
                    var response = _mapper.Map<AvailabilityMessagingContracts.Messages.Manager.UnavailableSkusResponse>(outbound.Value);
                    await context.RespondAsync(response);
                }

                _logger.LogInformation("Success on get [{count} of {total}] sku(s) main contract unavailable", outbound.Value.Skus.Count(), outbound.Value.Total);
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
