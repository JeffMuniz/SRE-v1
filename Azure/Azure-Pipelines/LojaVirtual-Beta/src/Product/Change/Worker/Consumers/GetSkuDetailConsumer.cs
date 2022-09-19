using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MessagingContracts = Shared.Messaging.Contracts;
using Usecases = Product.Change.Worker.Backend.Application.Usecases;

namespace Product.Change.Worker.Consumers
{
    public class GetSkuDetailConsumer : IConsumer<MessagingContracts.Product.Change.Messages.GetSkuDetail>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecases.GetSkuDetail.IGetSkuDetailUsecase _getSkuDetailUsecase;

        public GetSkuDetailConsumer(
            ILogger<GetSkuDetailConsumer> logger,
            IMapper mapper,
            Usecases.GetSkuDetail.IGetSkuDetailUsecase getSkuDetailUsecase
        )
        {
            _logger = logger;
            _mapper = mapper;
            _getSkuDetailUsecase = getSkuDetailUsecase;
        }

        public async Task Consume(ConsumeContext<MessagingContracts.Product.Change.Messages.GetSkuDetail> context)
        {
            try
            {
                var inbound = _mapper.Map<Usecases.GetSkuDetail.Models.Inbound>(context.Message);

                var result = await _getSkuDetailUsecase.Execute(inbound, context.CancellationToken);
                if (result.IsFailure)
                {
                    _logger.LogWarning("Consumed with failure! Failure: {Error}, {Sku}", result.Error, new { inbound.SkuIntegrationId });

                    if (context.IsResponseAccepted<MessagingContracts.Shared.Messages.NotFound>())
                    {
                        var notFoundOutput = _mapper.Map<MessagingContracts.Shared.Messages.NotFound>(result.Error);
                        await context.RespondAsync(notFoundOutput);
                    }

                    return;
                }

                if (context.IsResponseAccepted<MessagingContracts.Product.Change.Messages.GetSkuDetailResponse>())
                {
                    var output = _mapper.Map<MessagingContracts.Product.Change.Messages.GetSkuDetailResponse>(result.Value);
                    await context.RespondAsync(output);
                }

                _logger.LogInformation("Successfully consumed! {Sku}", new { inbound.SkuIntegrationId, result.Value.SupplierId, result.Value.SkuId });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message! {Sku}", new { context.Message.SkuIntegrationId });

                if (context.IsResponseAccepted<MessagingContracts.Shared.Messages.UnexpectedError>())
                {
                    var unexpectedErrorOutput = _mapper.Map<MessagingContracts.Shared.Messages.UnexpectedError>(error);
                    await context.RespondAsync(unexpectedErrorOutput);
                }
            }
        }
    }
}
