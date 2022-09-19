using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Persistence.Mongo.Configurations;
using Shared.Persistence.Mongo.Shared;
using System;

namespace Shared.Persistence.Mongo.Extensions
{
    public static class PersistenceMongoExtensions
    {
        public static IServiceCollection AddMongoContext<TContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<MongoOptions> postConfigureOptions = null
        ) where TContext : class, IMongoContext =>
            services
                .AddMongoContext<TContext, MongoOptions>(configuration, postConfigureOptions);

        public static IServiceCollection AddMongoContext<TContext>(
            this IServiceCollection services,
            string contextName,
            IConfiguration configuration,
            Action<MongoOptions> postConfigureOptions = null
        ) where TContext : class, IMongoContext =>
            services
                .AddMongoContext<TContext, MongoOptions>(contextName, configuration, postConfigureOptions);

        public static IServiceCollection AddMongoContext<TContext, TContextSettings>(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<TContextSettings> postConfigureOptions = null
        ) where TContext : class, IMongoContext
          where TContextSettings : class, IMongoOptions =>
            services
                .AddMongoContext<TContext, TContextSettings>(NameProvider.GetContextName<TContext>(), configuration, postConfigureOptions);

        public static IServiceCollection AddMongoContext<TContext, TContextSettings>(
            this IServiceCollection services,
            string contextName,
            IConfiguration configuration,
            Action<TContextSettings> postConfigureOptions = null
        ) where TContext : class, IMongoContext
          where TContextSettings : class, IMongoOptions
        {
            services
                .AddSingleton<TContext>()
                .AddOptions()
                .Configure<TContextSettings>(contextName, configuration);

            if (postConfigureOptions != null)
                services.PostConfigure(contextName, postConfigureOptions);

            return services;
        }
    }
}
