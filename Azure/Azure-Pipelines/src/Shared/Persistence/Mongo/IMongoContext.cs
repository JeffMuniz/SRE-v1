using MongoDB.Driver;
using Shared.Persistence.Mongo.Configurations;

namespace Shared.Persistence.Mongo
{
    public interface IMongoContext<TOptions> : IMongoContext
        where TOptions : class, IMongoOptions
    {
        TOptions Options { get; }
    }

    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}
