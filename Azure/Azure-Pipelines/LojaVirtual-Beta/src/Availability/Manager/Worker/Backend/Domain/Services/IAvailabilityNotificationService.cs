using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Domain.Services
{
    public interface IAvailabilityNotificationService
    {
        Task NotifyAvailabilityChanged(Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task SendGetAvailability(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task SendGetLatestAvailability(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);
    }
}
