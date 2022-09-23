using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MessagingContracts = Shared.Messaging.Contracts;
using Usecases = Product.Change.Worker.Backend.Application.Usecases;

namespace Product.Change.Worker.Consumers
{
    public class IntegrateSkuConsumer : IConsumer<MessagingContracts.Product.Change.Messages.IntegrateSku>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Usecases.IntegrateSku.IIntegrateSkuUsecase _integrateSkuUsecase;

        public IntegrateSkuConsumer(
            ILogger<IntegrateSkuConsumer> logger,
            IMapper mapper,
            Usecases.IntegrateSku.IIntegrateSkuUsecase integrateSkuUsecase
        )
        {
            _logger = logger;
            _mapper = mapper;
            _integrateSkuUsecase = integrateSkuUsecase;
        }

        public async Task Consume(ConsumeContext<MessagingContracts.Product.Change.Messages.IntegrateSku> context)
        {
            try
            {
                var inbound = _mapper.Map<Usecases.IntegrateSku.Models.Inbound>(context.Message);

                var result = await _integrateSkuUsecase.Execute(inbound, context.CancellationToken);
                if (result.IsFailure)
                {
                    _logger.LogWarning("Consumed with failure! Failure: {Error}, {Sku}", result.Error, new { inbound.SupplierId, inbound.SkuId });

                    return;
                }

                _logger.LogInformation("Successfully consumed! {Sku}", new { inbound.SupplierId, inbound.SkuId });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Unexpected error consuming message! {Sku}", new { context.Message.SupplierSku.SupplierId, context.Message.SupplierSku.SkuId });

                throw;
            }
        }
    }
}
