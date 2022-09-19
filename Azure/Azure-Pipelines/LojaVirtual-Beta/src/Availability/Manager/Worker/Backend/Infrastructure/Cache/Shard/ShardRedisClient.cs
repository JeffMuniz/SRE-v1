using StackExchange.Redis.Extensions.Core.Abstractions;
using System;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard
{
    public class ShardRedisClient : IShardRedisClient
    {
        private readonly Lazy<IRedisClient> _lazyRedisClient;

        public ShardName ShardName { get; }

        public IRedisClient Client
            => _lazyRedisClient.Value;

        public ShardRedisClient(ShardName shardName, Func<IRedisClient> valueFactory)
        {
            ShardName = shardName;
            _lazyRedisClient = new Lazy<IRedisClient>(valueFactory);
        }
    }
}
