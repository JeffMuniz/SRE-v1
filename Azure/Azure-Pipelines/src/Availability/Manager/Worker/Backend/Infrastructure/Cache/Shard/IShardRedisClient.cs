using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard
{
    public interface IShardRedisClient
    {
        ShardName ShardName { get; }

        IRedisClient Client { get; }
    }
}
