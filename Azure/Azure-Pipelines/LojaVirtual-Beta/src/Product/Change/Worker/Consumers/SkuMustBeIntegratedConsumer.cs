using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MessagingContracts = Shared.Messaging.Contracts;
using Usecases = Product.Change.Worker.Backend.Application.Usecases;

namespace Product.Change.Worker.Consumers
{
    public class SkuMustBeIntegratedConsumer : IConsumer<MessagingContracts.Product.Change.Messages.SkuMustBeIntegrated>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecases.SkuMustBeIntegrated.ISkuMustBeIntegratedUsecase _productMustBeIntegratedUsecase;

        public SkuMustBeIntegratedConsumer(
            ILogger<SkuMustBeIntegratedConsumer> logger,
            IMapper mapper,
            Usecases.SkuMustBeIntegrated.ISkuMustBeIntegratedUsecase productMustBeIntegratedUsecase
        )
        {
            _logger = logger;
            _mapper = mapper;
            _productMustBeIntegratedUsecase = productMustBeIntegratedUsecase;
        }

        public async Task Consume(ConsumeContext<MessagingContracts.Product.Change.Messages.SkuMustBeIntegrated> context)
        {
            try
            {
                var inbound = _mapper.Map<Usecases.SkuMustBeIntegrated.Models.Inbound>(context.Message);

                var result = await _productMustBeIntegratedUsecase.Execute(inbound, context.CancellationToken);
                if (result.IsFailure)
                {
                    _logger.LogWarning("Consumed with failure! Failure: {Error}, {Sku}", result.Error, new { inbound.SupplierId, SkuId = inbound.SupplierSkuId });

                    if (context.IsResponseAccepted<MessagingContracts.Shared.Messages.NotFound>())
                    {
                        var notFoundOutput = _mapper.Map<MessagingContracts.Shared.Messages.NotFound>(result.Error);
                        await context.RespondAsync(notFoundOutput);
                    }

                    return;
                }

                if (context.IsResponseAccepted<MessagingContracts.Product.Change.Messages.SkuMustBeIntegratedResponse>())
                {
                    var output = _mapper.Map<MessagingContracts.Product.Change.Messages.SkuMustBeIntegratedResponse>(result.Value);
                    await context.RespondAsync(output);
                }

                _logger.LogInformation("Successfully consumed! {Sku}", new { inbound.SupplierId, SkuId = inbound.SupplierSkuId });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message! {Sku}", new { context.Message.SupplierId, SkuId = context.Message.SupplierSkuId });

                if (context.IsResponseAccepted<MessagingContracts.Shared.Messages.UnexpectedError>())
                {
                    var unexpectedErrorOutput = _mapper.Map<MessagingContracts.Shared.Messages.UnexpectedError>(error);
                    await context.RespondAsync(unexpectedErrorOutput);
                }
            }
        }
    }
}
