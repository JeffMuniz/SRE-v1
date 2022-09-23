using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product.Saga.Worker.Backend.Infrastructure.Persistence.Entities;
using Shared.Persistence.Mongo;
using Shared.Persistence.Mongo.Configurations;

namespace Product.Saga.Worker.Backend.Infrastructure.Persistence.Contexts
{
    public class IntegrationContext : MongoContext<IntegrationContext, MongoOptions>, IIntegrationContext
    {
        public IntegrationContext(IOptionsMonitor<MongoOptions> settings)
            : base(settings)
        {
        }

        public IMongoCollection<SkuSagaHistory> SkuSagaHistory =>
            GetCollection<SkuSagaHistory>();
    }
}
