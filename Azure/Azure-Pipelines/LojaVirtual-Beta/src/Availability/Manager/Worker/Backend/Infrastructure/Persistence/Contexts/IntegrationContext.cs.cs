using Availability.Manager.Worker.Backend.Infrastructure.Persistence.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Persistence.Mongo;
using Shared.Persistence.Mongo.Configurations;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Contexts
{
    public class IntegrationContext : MongoContext<IntegrationContext, MongoOptions>, IIntegrationContext
    {
        public IntegrationContext(IOptionsMonitor<MongoOptions> settings)
            : base(settings)
        {
        }

        public IMongoCollection<Sku> Sku =>
            GetCollection<Sku>();

        public IMongoCollection<CacheRenewSchedule> CacheRenewSchedule =>
            GetCollection<CacheRenewSchedule>();
    }
}
