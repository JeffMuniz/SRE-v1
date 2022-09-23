using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolrNet;

namespace Goatling.Solr.Enriched
{
    public static class EnrichedExtensions
    {
        public static IServiceCollection AddServicesProductsEnriched(this IServiceCollection services, IConfiguration config)
        {
            services.AddSolrNet<SearchIndexModel>(config.GetSection("Solr:Destination").Value);
            return services;
        }
    }
}
