using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolrNet;

namespace Goatling.Solr.Products
{
    public static class DocExtensions
    {
        public static IServiceCollection AddServicesProducts(this IServiceCollection services, IConfiguration config)
        {
            services.AddSolrNet<Doc>(config.GetSection("Solr:Source").Value);
            return services;
        }
    }
}
