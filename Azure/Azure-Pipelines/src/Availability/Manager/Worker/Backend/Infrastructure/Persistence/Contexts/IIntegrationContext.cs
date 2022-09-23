using Availability.Manager.Worker.Backend.Infrastructure.Persistence.Entities;
using MongoDB.Driver;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Contexts
{
    public interface IIntegrationContext
    {
        IMongoCollection<Sku> Sku { get; }

        IMongoCollection<CacheRenewSchedule> CacheRenewSchedule { get; }
    }
}
