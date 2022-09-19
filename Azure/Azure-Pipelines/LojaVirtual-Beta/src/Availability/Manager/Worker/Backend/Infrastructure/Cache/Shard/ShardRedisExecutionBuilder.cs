using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard
{
    public class ShardRedisExecutionBuilder : IShardRedisExecutionBuilder.IExecution
        , IShardRedisExecutionBuilder.IConnectionManagerBuilder
        , IShardRedisExecutionBuilder.IShardIdBuilder
        , IShardRedisExecutionBuilder.IDatabaseIdBuilder
        , IShardRedisExecutionBuilder.ICancellationBuilder
    {
        private IShardRedisConnectionManager ConnectionManager { get; }

        private Domain.ValueObjects.ShardId ShardId { get; set; }

        private int DatabaseId { get; set; } = 0;

        private CancellationToken CancellationToken { get; set; } = CancellationToken.None;

        private IEnumerable<ShardName> ShardNames { get; set; }

        public ShardRedisExecutionBuilder(IShardRedisConnectionManager shardRedisConnectionManager)
        {
            ConnectionManager = shardRedisConnectionManager;
        }

        public static IShardRedisExecutionBuilder.IConnectionManagerBuilder Create(IShardRedisConnectionManager shardRedisConnectionManager)
            => new ShardRedisExecutionBuilder(shardRedisConnectionManager);

        public IShardRedisExecutionBuilder.IShardIdBuilder FromShardId(Domain.ValueObjects.ShardId shardId)
        {
            ShardId = shardId;

            return this;
        }

        public IShardRedisExecutionBuilder.IDatabaseIdBuilder FromDatabaseId(int databaseId)
        {
            DatabaseId = databaseId;

            return this;
        }

        public IShardRedisExecutionBuilder.ICancellationBuilder WithCancellation(CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;

            return this;
        }

        public IShardRedisExecutionBuilder.IExecution Build()
        {
            ShardNames = GetShardNames(ShardId);

            return this;
        }

        public async Task Execute(
            Func<IRedisDatabase, Task> executeHandle
        )
        {
            foreach (var shardName in ShardNames)
            {
                CancellationToken.ThrowIfCancellationRequested();
                var shard = ConnectionManager.GetShard(shardName);
                await executeHandle(shard.Client.GetDb(DatabaseId));
            }
        }

        public async IAsyncEnumerable<KeyValuePair<ShardName, TResult>> Execute<TResult>(
            Func<IRedisDatabase, Task<TResult>> executeHandle
        )
        {
            foreach (var shardName in ShardNames)
            {
                CancellationToken.ThrowIfCancellationRequested();
                var shard = ConnectionManager.GetShard(shardName);
                yield return new KeyValuePair<ShardName, TResult>(
                    shardName,
                    await executeHandle(shard.Client.GetDb(DatabaseId))
                );
            }
        }

        private IEnumerable<ShardName> GetShardNames(
            Domain.ValueObjects.ShardId shardId
        )
        {
            if (shardId == Domain.ValueObjects.ShardId.AllShards)
                return ConnectionManager.GetAllShardNames();

            if (string.IsNullOrWhiteSpace(shardId?.Id))
                return new[] { ConnectionManager.GetDefaultShardName() };

            return new ShardName[] { shardId.Id };
        }
    }
}
