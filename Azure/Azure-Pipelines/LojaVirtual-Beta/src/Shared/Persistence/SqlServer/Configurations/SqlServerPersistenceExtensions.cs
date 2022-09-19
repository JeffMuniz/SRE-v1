using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shared.Persistence.SqlServer.Configurations
{
    public static class SqlServerPersistenceExtensions
    {
        public static IServiceCollection AddPersistenceSqlServer<TContext>(this IServiceCollection services, Action<SqlServerOptions> sqlServerOptions)
            where TContext : class, ISqlServerContext
        {
            services
                .Configure(sqlServerOptions)
                .AddScoped<TContext>();

            return services;
        }
    }
}
