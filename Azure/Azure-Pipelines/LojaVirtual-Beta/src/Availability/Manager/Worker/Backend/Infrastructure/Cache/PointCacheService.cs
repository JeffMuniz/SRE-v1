using Availability.Manager.Worker.Backend.Domain.Services;
using Availability.Manager.Worker.Backend.Infrastructure.Cache.Resiliency;
using Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard;
using Availability.Manager.Worker.Configurations.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache
{
    public class PointCacheService : CacheBase, IPointCacheService
    {
        public PointCacheService(
            ILogger<PointCacheService> logger,
            IRetryPolicy retryPolicy,
            IOptionsMonitor<CacheConfigurationOptions> options,
            IShardRedisConnectionManager shardRedisConnectionManager
        )
            : base(logger, retryPolicy, options, shardRedisConnectionManager)
        {
        }

        public async Task<bool> Remove(Domain.ValueObjects.ShardId shardId, Domain.Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken) =>
            await GetShardRedisExecution(shardId, cancellationToken)
                .Execute(async db =>
                {
                    var keys = await SearchKeys(db, skuAvailability.Id.SupplierId, skuAvailability.Id.ContractId, skuAvailability.PersistedSkuId);

                    return await RemoveAll(db, keys, cancellationToken);
                })
                .AllAsync(x => x.Value);

        public async Task<bool> RemoveAllContracts(Domain.ValueObjects.ShardId shardId, Domain.Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken) =>
            await GetShardRedisExecution(shardId, cancellationToken)
                .Execute(async db =>
                {
                    var keys = await SearchKeys(db, skuAvailability.Id.SupplierId, skuAvailability.PersistedSkuId);

                    return await RemoveAll(db, keys, cancellationToken);
                })
                .AllAsync(x => x.Value);

        public async Task<bool> CheckInUse(
            Domain.ValueObjects.ShardId shardId,
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var renewIdleTime = _options.CurrentValue.Availability.Renew.IdleTime;

            var keyInUse = await GetShardRedisExecution(shardId, cancellationToken)
                .Execute(async db =>
                {
                    var keys = await SearchKeys(db, skuAvailability.Id.SupplierId, skuAvailability.Id.ContractId, skuAvailability.PersistedSkuId);

                    return keys
                        .AsParallel()
                        .WithCancellation(cancellationToken)
                        .WithDegreeOfParallelism(_options.CurrentValue.KeysPerPage)
                        .Select(key => db.Database.KeyIdleTimeAsync(key))
                        .Any(idleTime => idleTime.Result.GetValueOrDefault(renewIdleTime) < renewIdleTime);
                })
                .AnyAsync(x => x.Value, cancellationToken);

            return keyInUse;
        }

        private Task<bool> RemoveAll(IRedisDatabase db, IEnumerable<string> keys, CancellationToken cancellationToken) =>
            ExecutePagedAllKeysToCache(keys,
                async cacheKeys => await db.RemoveAllAsync(cacheKeys.AsArray()) > 0
                , cancellationToken
            );

        private Task<IEnumerable<string>> SearchKeys(IRedisDatabase db, int supplierId, string supplierContractId, string persistedSkuId) =>
            db.SearchKeysAsync(CreateSearchPointKey(supplierId, supplierContractId, persistedSkuId));

        private Task<IEnumerable<string>> SearchKeys(IRedisDatabase db, int supplierId, string persistedSkuId) =>
            db.SearchKeysAsync(CreateSearchPointKey(supplierId, persistedSkuId));

        private IShardRedisExecutionBuilder.IExecution GetShardRedisExecution(Domain.ValueObjects.ShardId shardId, CancellationToken cancellation) =>
            GetShardRedisExecution(shardId, _options.CurrentValue.Availability.Points.DbNumber, cancellation);

        private static string CreateSearchPointKey(int supplierId, string supplierContractId, string persistedSkuId)
         => $"Points.Availability:vendor:{supplierId}:contract:{supplierContractId}:catalog:*:sku:{persistedSkuId}";

        private static string CreateSearchPointKey(int supplierId, string persistedSkuId)
         => $"Points.Availability:vendor:{supplierId}:contract:*:catalog:*:sku:{persistedSkuId}";
    }
}
