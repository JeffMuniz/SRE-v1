using Availability.Manager.Worker.Backend.Infrastructure.Cache.Resiliency;
using Availability.Manager.Worker.Backend.Infrastructure.Cache.Shard;
using Availability.Manager.Worker.Configurations.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Availability.Manager.Worker.Configurations
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedis(
            this IServiceCollection services,
            IConfigurationSection redisConfiguration
        )
        {
            services
                .AddOptions<RedisConfigurationOptions>()
                .Bind(redisConfiguration)
                .Configure(redisCfg =>
                {
                    var retryTime = (int)redisCfg.RetryTime.TotalMilliseconds;
                    redisCfg.ConfigurationOptions.ReconnectRetryPolicy = new ExponentialRetry(retryTime);
                })
                .Services
                .AddOptions<RedisShardConfigurationOptions>()
                .Configure<IOptionsMonitor<RedisConfigurationOptions>>((shardCfg, redisCfg) =>
                {
                    shardCfg.DefaultInstanceName = redisCfg.CurrentValue.Shards.DefaultInstanceName;
                    shardCfg.Instances = redisCfg.CurrentValue.Shards.Instances;
                });

            services
                .AddSingleton<IRetryPolicy, RetryPolicy>()
                .AddSingleton<ISerializer, NewtonsoftSerializer>()
                .AddSingleton<IShardRedisConnectionManager, ShardRedisConnectionManager>();

            return services;
        }
    }
}
