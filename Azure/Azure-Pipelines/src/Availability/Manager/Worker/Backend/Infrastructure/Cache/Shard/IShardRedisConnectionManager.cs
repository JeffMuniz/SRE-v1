using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard
{
    public interface IShardRedisConnectionManager : IDisposable
    {
        ShardName GetDefaultShardName();

        IEnumerable<ShardName> GetAllShardNames();

        IShardRedisClient GetShard(ShardName shardName);

        bool TryGetShard(ShardName shardName, out IShardRedisClient shardRedisClient);
    }
}
