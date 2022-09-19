using Availability.Manager.Worker.Backend.Domain.Services;
using Availability.Manager.Worker.Backend.Infrastructure.Cache;
using Availability.Manager.Worker.Configurations.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Availability.Manager.Worker.Configurations
{
    public static class CacheExtensions
    {
        public static IServiceCollection AddCache(
            this IServiceCollection services,
            IConfigurationSection cacheConfiguration
        )
        {
            services
                .Configure<CacheConfigurationOptions>(cacheConfiguration)
                .AddSingleton<ICashCacheService, CashCacheService>()
                .AddSingleton<IPointCacheService, PointCacheService>();

            return services;
        }
    }
}
