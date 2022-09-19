using MongoDB.Driver;
using Product.Saga.Worker.Backend.Infrastructure.Persistence.Entities;
using Shared.Persistence.Mongo;
using Shared.Persistence.Mongo.Configurations;

namespace Product.Saga.Worker.Backend.Infrastructure.Persistence.Contexts
{
    public interface IIntegrationContext : IMongoContext<MongoOptions>
    {
        IMongoCollection<SkuSagaHistory> SkuSagaHistory { get; }
    }
}
