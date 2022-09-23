using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Services;
using Availability.Manager.Worker.Backend.Infrastructure.Cache.Models;
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
    public class CashCacheService : CacheBase, ICashCacheService
    {
        private readonly IMapper _mapper;

        public CashCacheService(
            ILogger<CashCacheService> logger,
            IRetryPolicy retryPolicy,
            IOptionsMonitor<CacheConfigurationOptions> options,
            IShardRedisConnectionManager shardRedisConnectionManager,
            IMapper mapper
        )
            : base(logger, retryPolicy, options, shardRedisConnectionManager)
        {
            _mapper = mapper;
        }

        public Task<bool> Add(Domain.ValueObjects.ShardId shardId, Domain.Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken) =>
            Add(shardId,
                skuAvailability,
                DateTime.Now.Add(_options.CurrentValue.Availability.Cash.ExpiresTime),
                cancellationToken
            );

        public async Task<bool> Add(Domain.ValueObjects.ShardId shardId, Domain.Entities.SkuAvailability skuAvailability, DateTime expiresAt, CancellationToken cancellationToken)
        {
            var cashAvailability = _mapper.Map<CashAvailability>(skuAvailability);
            var cacheExpiresOn = ConvertToTimeSpanUtcCacheExpires(expiresAt);
            var key = CreateCashKey(skuAvailability.Id.SupplierId, skuAvailability.Id.ContractId, skuAvailability.PersistedSkuId);

            return await ExecuteKeyToCache(key,
                async cacheKey =>
                    await GetShardRedisExecution(shardId, cancellationToken)
                        .Execute(
                            db => db.AddAsync(cacheKey, cashAvailability, cacheExpiresOn.Value, StackExchange.Redis.When.Always)
                        )
                        .AllAsync(x => x.Value)
                , cancellationToken
            );
        }

        public async Task<bool> Remove(Domain.ValueObjects.ShardId shardId, Domain.Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken)
        {
            var key = CreateCashKey(skuAvailability.Id.SupplierId, skuAvailability.Id.ContractId, skuAvailability.PersistedSkuId);

            return await ExecuteKeyToCache(key,
                async cacheKey =>
                    await GetShardRedisExecution(shardId, cancellationToken)
                        .Execute(
                            db => db.RemoveAsync(cacheKey)
                        )
                        .AllAsync(x => x.Value)
                , cancellationToken
            );
        }

        public async Task<bool> RemoveAllContracts(Domain.ValueObjects.ShardId shardId, Domain.Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken) =>
            await GetShardRedisExecution(shardId, cancellationToken)
                .Execute(async db =>
                {
                    var keys = await SearchKeys(db, skuAvailability.Id.SupplierId, skuAvailability.PersistedSkuId);

                    return await ExecutePagedAllKeysToCache(keys,
                        async cacheKeys => await db.RemoveAllAsync(cacheKeys.AsArray()) > 0
                        , cancellationToken
                    );
                })
                .AllAsync(x => x.Value);

        public Task<bool> Renew(Domain.ValueObjects.ShardId shardId, Domain.Entities.SkuAvailability skuAvailability, CancellationToken cancellationToken) =>
            Renew(shardId,
                skuAvailability,
                DateTime.Now.Add(_options.CurrentValue.Availability.Cash.ExpiresTime),
                cancellationToken
            );

        public async Task<bool> Renew(Domain.ValueObjects.ShardId shardId, Domain.Entities.SkuAvailability skuAvailability, DateTime expiresAt, CancellationToken cancellationToken)
        {
            var cacheExpiresOn = ConvertToTimeSpanUtcCacheExpires(expiresAt);
            var key = CreateCashKey(skuAvailability.Id.SupplierId, skuAvailability.Id.ContractId, skuAvailability.PersistedSkuId);

            return await ExecuteKeyToCache(
                key,
                async cacheKey =>
                    await GetShardRedisExecution(shardId, cancellationToken)
                        .Execute(
                            db => db.UpdateExpiryAsync(cacheKey, cacheExpiresOn.Value)
                        )
                        .AllAsync(x => x.Value)
                , cancellationToken
            );
        }

        public async Task<bool> CheckInUse(
            Domain.ValueObjects.ShardId shardId,
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var renewIdleTime = _options.CurrentValue.Availability.Renew.IdleTime;
            var key = CreateCashKey(skuAvailability.Id.SupplierId, skuAvailability.Id.ContractId, skuAvailability.PersistedSkuId);

            return await GetShardRedisExecution(shardId, cancellationToken)
                .Execute(
                    db => db.Database.KeyIdleTimeAsync(key)
                )
                .Select(x => x.Value)
                .AnyAsync(idleTime =>
                    idleTime.GetValueOrDefault(renewIdleTime) < renewIdleTime
                    , cancellationToken
                );
        }

        private Task<IEnumerable<string>> SearchKeys(IRedisDatabase db, int supplierId, string persistedSkuId) =>
            db.SearchKeysAsync(CreateSearchCacheKey(supplierId, persistedSkuId));

        private IShardRedisExecutionBuilder.IExecution GetShardRedisExecution(Domain.ValueObjects.ShardId shardId, CancellationToken cancellation) =>
            GetShardRedisExecution(shardId, _options.CurrentValue.Availability.Cash.DbNumber, cancellation);

        private static string CreateCashKey(int supplierId, string supplierContractId, string persistedSkuId) =>
            $"Cash.Availability:vendor:{supplierId}:contract:{supplierContractId}:sku:{persistedSkuId}";

        private static string CreateSearchCacheKey(int supplierId, string persistedSkuId) =>
            $"Cash.Availability:vendor:{supplierId}:contract:*:sku:{persistedSkuId}";
    }
}
