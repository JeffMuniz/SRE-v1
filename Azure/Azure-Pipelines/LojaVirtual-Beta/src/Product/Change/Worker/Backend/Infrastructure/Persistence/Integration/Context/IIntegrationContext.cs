using MongoDB.Driver;
using Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Entities;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Context
{
    public interface IIntegrationContext
    {
        IMongoCollection<SkuIntegration> SkuIntegration { get; }
    }
}
