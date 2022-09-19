using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard
{
    public interface IShardRedisExecutionBuilder
    {
        public interface IConnectionManagerBuilder
        {
            IShardIdBuilder FromShardId(Domain.ValueObjects.ShardId shardId);
        }

        public interface IShardIdBuilder
        {
            IDatabaseIdBuilder FromDatabaseId(int databaseId);
        }

        public interface IDatabaseIdBuilder
        {
            ICancellationBuilder WithCancellation(CancellationToken cancellationToken);

            IExecution Build();
        }

        public interface ICancellationBuilder
        {
            IExecution Build();
        }

        public interface IExecution
        {
            Task Execute(Func<IRedisDatabase, Task> executeHandle);

            IAsyncEnumerable<KeyValuePair<ShardName, TResult>> Execute<TResult>(
                Func<IRedisDatabase, Task<TResult>> executeHandle
            );
        }
    }
}
