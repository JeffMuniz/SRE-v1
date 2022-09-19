using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MessagingContracts = Shared.Messaging.Contracts;
using Usecases = Product.Enrichment.macnaima.Worker.Backend.Application.Usecases;

namespace Product.Enrichment.macnaima.Worker.Consumers
{
    public class SendSkuForEnrichmentConsumer : IConsumer<MessagingContracts.Product.Saga.Messages.Enrichment.SendSkuForEnrichment>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecases.NotifyPendingOffer.INotifyPendingOfferUsecase _notifyPendingOfferUsecase;

        public SendSkuForEnrichmentConsumer(
            ILogger<SendSkuForEnrichmentConsumer> logger,
            IMapper mapper,
            Usecases.NotifyPendingOffer.INotifyPendingOfferUsecase notifyPendingOfferUsecase
        )
        {
            _logger = logger;
            _mapper = mapper;
            _notifyPendingOfferUsecase = notifyPendingOfferUsecase;
        }

        public async Task Consume(ConsumeContext<MessagingContracts.Product.Saga.Messages.Enrichment.SendSkuForEnrichment> context)
        {
            try
            {
                _logger.LogDebug("Starting product enrichment notification: {message}", context.Message);

                var inbound = _mapper.Map<Usecases.NotifyPendingOffer.Models.Inbound>(context.Message);

                var result = await _notifyPendingOfferUsecase.Execute(inbound, context.CancellationToken);
                if (result.IsFailure)
                {
                    _logger.LogWarning("Failure on notify offer {SkuIntegrationId}. Error: {Error}", context.Message.SkuIntegrationId, result.Error);

                    return;
                }

                _logger.LogInformation("Success on notify offer {SkuIntegrationId}", context.Message.SkuIntegrationId);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message");

                throw;
            }
        }
    }
}
