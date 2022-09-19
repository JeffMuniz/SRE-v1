using AutoMapper;
using MassTransit;
using Product.Change.Worker.Backend.Domain.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Change.Worker.Backend.Infrastructure.Messaging
{
    public class SkuNotificationService : ISkuNotificationService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public SkuNotificationService(
            IMapper mapper,
            IBus bus
        )
        {
            _mapper = mapper;
            _bus = bus;
        }

        public async Task NotifyChangedPrice(Domain.Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken)
        {
            var skuPriceChangedMessage = _mapper.Map<Shared.Messaging.Contracts.Product.Change.Messages.SkuPriceChanged>(skuIntegration);
            
            await _bus.Publish(skuPriceChangedMessage, cancellationToken);
        }

        public async Task NotifyAddSku(Domain.Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken)
        {
            var addSkuMessage = _mapper.Map<Shared.Messaging.Contracts.Product.Saga.Messages.Change.AddSku>(skuIntegration);

            await _bus.Send(addSkuMessage, cancellationToken);
        }

        public async Task NotifyUpdateSku(Domain.Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken)
        {
            var updateSkuMessage = _mapper.Map<Shared.Messaging.Contracts.Product.Saga.Messages.Change.UpdateSku>(skuIntegration);

            await _bus.Send(updateSkuMessage, cancellationToken);
        }

        public async Task NotifyRemoveSku(Domain.Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken)
        {
            var inactivateSkuMessage = _mapper.Map<Shared.Messaging.Contracts.Product.Saga.Messages.Change.RemoveSku>(skuIntegration);

            await _bus.Send(inactivateSkuMessage, cancellationToken);
        }
    }
}
