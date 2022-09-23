using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Entities;
using Shared.Persistence.Mongo;
using Shared.Persistence.Mongo.Configurations;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Context
{
    public class IntegrationContext : MongoContext<IntegrationContext, MongoOptions>, IIntegrationContext
    {
        public IntegrationContext(IOptionsMonitor<MongoOptions> settings)
            : base(settings)
        {
        }

        public IMongoCollection<SkuIntegration> SkuIntegration =>
            GetCollection<SkuIntegration>();
    }
}
