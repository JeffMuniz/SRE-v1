using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Services;
using MassTransit;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using System.Threading;
using System.Threading.Tasks;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Manager.Worker.Backend.Infrastructure.Messaging
{
    public class AvailabilityNotificationService : IAvailabilityNotificationService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public AvailabilityNotificationService(
            IMapper mapper,
            IBus bus
        )
        {
            _mapper = mapper;
            _bus = bus;
        }

        public async Task NotifyAvailabilityChanged(Domain.Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken)
        {
            var availabilityChangedMessage = _mapper.Map<AvailabilityChanged>(skuAvailability);

            await _bus.Publish(availabilityChangedMessage, cancellationToken);
        }

        public async Task SendGetAvailability(
            Domain.ValueObjects.ShardId shardId,
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var getAvailabilityMessage = _mapper.Map<AvailabilityMessages.Search.GetAvailability>(shardId, skuAvailability);

            await _bus.Send(getAvailabilityMessage, cancellationToken);
        }

        public async Task SendGetLatestAvailability(
            Domain.ValueObjects.ShardId shardId,
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var getLatestAvailabilityMessage = _mapper.Map<GetLatestAvailability>(shardId, skuAvailability);

            await _bus.Send(getLatestAvailabilityMessage, cancellationToken);
        }
    }
}
