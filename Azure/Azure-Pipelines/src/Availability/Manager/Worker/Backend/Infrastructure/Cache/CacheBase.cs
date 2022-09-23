using Availability.Manager.Worker.Backend.Infrastructure.Cache.Resiliency;
using Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard;
using Availability.Manager.Worker.Configurations.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache
{
    public abstract class CacheBase
    {
        protected readonly ILogger _logger;
        protected readonly IRetryPolicy _retryPolicy;
        protected readonly IOptionsMonitor<CacheConfigurationOptions> _options;
        protected readonly IShardRedisConnectionManager _shardRedisConnectionManager;

        protected CacheBase(
            ILogger logger,
            IRetryPolicy retryPolicy,
            IOptionsMonitor<CacheConfigurationOptions> options,
            IShardRedisConnectionManager shardRedisConnectionManager
        )
        {
            _logger = logger;
            _retryPolicy = retryPolicy;
            _options = options;
            _shardRedisConnectionManager = shardRedisConnectionManager;
        }

        protected async Task<bool> ExecuteKeyToCache(
            string cacheKey,
            Func<string, Task<bool>> executePagedHandle,
            CancellationToken cancellationToken
        )
        {
            var result = true;
            try
            {
                result &= await _retryPolicy.Execute(
                    async () =>
                    {
                        if (await executePagedHandle(cacheKey))
                            return true;

                        throw new InvalidOperationException("RetryPolicy");
                    },
                    ("AvailabilityCache>ExecuteKeyToCache", _logger),
                    _options.CurrentValue.Resiliency.RetryAttempts,
                    cancellationToken
                );
            }
            catch (InvalidOperationException ex)
                when (ex.Message == "RetryPolicy")
            {
                result = false;
            }

            return result;
        }

        protected Task<bool> ExecutePagedAllKeysToCache(
           IEnumerable<string> cacheKeys,
           Func<IEnumerable<string>, Task<bool>> executePagedHandle,
           CancellationToken cancellationToken
        ) => ExecutePagedAllKeysToCache(cacheKeys, _options.CurrentValue.KeysPerPage, executePagedHandle, cancellationToken);

        protected async Task<bool> ExecutePagedAllKeysToCache(
           IEnumerable<string> cacheKeys,
           int keysPerPage,
           Func<IEnumerable<string>, Task<bool>> executePagedHandle,
           CancellationToken cancellationToken
        )
        {
            if (!cacheKeys.Any())
                return false;

            var totalPages = cacheKeys.Count() / keysPerPage + 1;

            var pages = Enumerable
                .Range(0, totalPages)
                .Select(page => (PageNumber: page, CacheKeys: cacheKeys.Skip(page * keysPerPage).Take(keysPerPage).ToArray()))
                .ToArray();

            var result = true;

            foreach (var (PageNumber, CacheKeys) in pages)
            {
                try
                {
                    result &= await _retryPolicy.Execute(
                        async () =>
                        {
                            if (await executePagedHandle(CacheKeys))
                                return true;

                            throw new InvalidOperationException("RetryPolicy");
                        },
                        ("AvailabilityCache>ExecutePagedAllKeysToCache", _logger),
                        _options.CurrentValue.Resiliency.RetryAttempts,
                        cancellationToken
                    );
                }
                catch (InvalidOperationException ex)
                    when (ex.Message == "RetryPolicy")
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        protected IShardRedisExecutionBuilder.IExecution GetShardRedisExecution(Domain.ValueObjects.ShardId shardId, int dbNumber, CancellationToken cancellation) =>
            _shardRedisConnectionManager
                .BuilderExecution()
                .FromShardId(shardId)
                .FromDatabaseId(dbNumber)
                .WithCancellation(cancellation)
                .Build();

        protected static TimeSpan? ConvertToTimeSpanUtcCacheExpires(DateTime? expiresAt) =>
           expiresAt.HasValue
               ? DateTimeOffset.FromFileTime(expiresAt.Value.ToFileTimeUtc()).Subtract(DateTimeOffset.UtcNow)
               : default(TimeSpan?);
    }
}
