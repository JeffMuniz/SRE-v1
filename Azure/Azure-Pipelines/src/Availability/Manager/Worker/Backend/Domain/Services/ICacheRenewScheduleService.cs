using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Domain.Services
{
    public interface ICacheRenewScheduleService
    {
        Task Schedule(ValueObjects.ShardId shardId, Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken);
    }
}
