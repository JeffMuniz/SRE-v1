using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Domain.Repositories
{
    public interface ICacheRenewScheduleRepository
    {
        Task<Maybe<Entities.CacheRenewSchedule>> Get(ValueObjects.CacheRenewScheduleId id, CancellationToken cancellationToken);

        Task<Entities.CacheRenewSchedule> Add(Entities.CacheRenewSchedule cacheRenewSchedule, CancellationToken cancellationToken);

        Task<bool> Update(Entities.CacheRenewSchedule cacheRenewSchedule, CancellationToken cancellationToken);
    }
}
