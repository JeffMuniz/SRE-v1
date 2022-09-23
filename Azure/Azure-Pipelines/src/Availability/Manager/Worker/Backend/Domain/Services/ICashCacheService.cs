using System;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Domain.Services
{
    public interface ICashCacheService
    {
        Task<bool> Add(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task<bool> Add(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, DateTime expiresAt, CancellationToken cancellationToken);

        Task<bool> Remove(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task<bool> RemoveAllContracts(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task<bool> Renew(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task<bool> Renew(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, DateTime expiresAt, CancellationToken cancellationToken);

        Task<bool> CheckInUse(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);
    }
}
