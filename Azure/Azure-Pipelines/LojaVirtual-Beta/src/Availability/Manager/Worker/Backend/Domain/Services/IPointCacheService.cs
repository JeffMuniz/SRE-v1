using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Domain.Services
{
    public interface IPointCacheService
    {
        Task<bool> Remove(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task<bool> RemoveAllContracts(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task<bool> CheckInUse(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);
    }
}
