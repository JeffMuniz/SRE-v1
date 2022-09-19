using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Implementations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard
{
    public class ShardRedisConnectionManager : IShardRedisConnectionManager
    {
        private readonly ILogger<RedisConnectionPoolManager> _connectionPoolManagerLogger;
        private readonly IOptionsMonitor<Configurations.Models.RedisConfigurationOptions> _options;
        private readonly ISerializer _serializer;
        private readonly List<IDisposable> _disposables = new();
        private readonly ConcurrentDictionary<ShardName, IShardRedisClient> _shardConnections = new();

        public ShardRedisConnectionManager(
            ILogger<RedisConnectionPoolManager> connectionPoolManagerLogger,
            IOptionsMonitor<Configurations.Models.RedisConfigurationOptions> options,
            ISerializer serializer
        )
        {
            _connectionPoolManagerLogger = connectionPoolManagerLogger;
            _options = options;
            _serializer = serializer;
            ConfigureShardsConnections();
        }

        public ShardName GetDefaultShardName() =>
            _options.CurrentValue.Shards.Instances
                .First(shard => shard.Name == _options.CurrentValue.Shards.DefaultInstanceName)
                .Name;

        public IEnumerable<ShardName> GetAllShardNames() =>
            _options.CurrentValue.Shards.Instances
                .Select(shard => new ShardName(shard.Name))
                .AsArray();

        public IShardRedisClient GetShard(ShardName shardName)
        {
            if (!TryGetShard(shardName, out var shardRedisClient))
                throw new KeyNotFoundException(nameof(shardName));

            return shardRedisClient;
        }

        public bool TryGetShard(ShardName shardName, out IShardRedisClient shardRedisClient) =>
            _shardConnections.TryGetValue(shardName, out shardRedisClient);

        private void ConfigureShardsConnections()
        {
            if (_shardConnections.Values.Any())
                throw new InvalidOperationException("Object has already been configured");

            foreach (var shardInstance in _options.CurrentValue.Shards.Instances)
            {
                _shardConnections.TryAdd(shardInstance.Name,
                    new ShardRedisClient(shardInstance.Name,
                        () =>
                        {
                            var redisConfiguration = _options.CurrentValue.Clone();
                            redisConfiguration.ConnectionString = shardInstance.ConnectionString;
                            redisConfiguration.KeyPrefix = string.Concat(redisConfiguration.KeyPrefix, shardInstance.KeyPrefix);

                            var redisConnectionPoolManager = new RedisConnectionPoolManager(redisConfiguration, _connectionPoolManagerLogger);

                            _disposables.Add(redisConnectionPoolManager);

                            return new RedisClient(redisConnectionPoolManager, _serializer, redisConfiguration);
                        }
                    )
                );
            }
        }

        #region [ Dispose ]

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                foreach (var disposable in _disposables)
                    disposable.Dispose();
            }

            _shardConnections.Clear();
            _disposables.Clear();

            _disposed = true;
        }

        ~ShardRedisConnectionManager()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion [ Dispose ]
    }
}
