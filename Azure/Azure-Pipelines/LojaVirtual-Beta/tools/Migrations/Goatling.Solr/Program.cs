using Goatling.Solr.Enriched;
using Goatling.Solr.Products;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Goatling.Solr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddServicesProducts(hostContext.Configuration);
                    services.AddServicesProductsEnriched(hostContext.Configuration);
                    services.AddHostedService<Worker>();
                });
    }
}
