using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Product.Supplier.Magalu.Worker.Backend.Application.Usecases.IntegrateFullCatalog;
using Product.Supplier.Magalu.Worker.Backend.Application.Usecases.IntegrateFullCatalog.Configurations;
using Product.Supplier.Magalu.Worker.Backend.Domain.Services;
using Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu;
using Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Configurations;
using Product.Supplier.Magalu.Worker.Jobs;
using Shared.Job.Abstractions.Product.Supplier.Magalu;
using Shared.Messaging.Configuration.Extensions;
using BaseSupplierWorker = Catalog.Integration.Product.Supplier.Shared.Worker;

namespace Product.Supplier.Magalu.Worker
{
    public class Startup : BaseSupplierWorker.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services
                .AddMessaging(
                    _configuration.GetConnectionString("ServiceBus")
                );            

            services
                .Configure<JobScheduleConfiguration>(_configuration.GetSection("Job"))
                .AddHostedService<MagaluIntegrateCatalogHostedService>()
                .AddScoped<IMagaluIntegrateCatalogRecurringJob, MagaluIntegrateCatalogRecurringJob>();

            services
                .Configure<MagaluConfigurationOptions>(_configuration.GetSection("Magalu"))
                .AddHttpClient<IMagaluIntegrationService, MagaluIntegrationService>((provider, client) =>
                {
                    var settings = provider.GetRequiredService<IOptionsMonitor<MagaluConfigurationOptions>>();
                    client.Timeout = settings.CurrentValue.Timeout;
                });

            services
                .Configure<IntegrateFullCatalogOptions>(_configuration.GetSection("Magalu:Catalog"))
                .AddScoped<IIntegrateFullCatalogUsecase, IntegrateFullCatalogUsecase>();
        }
    }
}
