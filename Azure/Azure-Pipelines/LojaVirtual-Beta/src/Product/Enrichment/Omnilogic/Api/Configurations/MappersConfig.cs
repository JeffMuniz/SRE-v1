using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Product.Enrichment.macnaima.Api.Configurations
{
    public static class MappersConfig
    {
        public static IServiceCollection AddCustomMappers(this IServiceCollection services)
        {
            var assembliesToScan = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Where(assemblyName => assemblyName.Name.StartsWith("Catalog.Integration", StringComparison.InvariantCultureIgnoreCase))
                .Select(assemblyName => Assembly.Load(assemblyName))
                .Prepend(Assembly.GetEntryAssembly());

            return services
                .AddAutoMapper(assembliesToScan);
        }
    }
}
