using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Supplier.Shared.Worker.Backend.Domain.Services;
using Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration;
using Shared.Job.Configuration.Extensions;
using Shared.Messaging.Configuration;
using System;
using BaseWorker = Shared.Worker;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;

namespace Catalog.Integration.Product.Supplier.Shared.Worker
{
    public abstract class Startup : BaseWorker.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddJobs(_configuration)
                .AddJobsServer(_configuration);

            EndpointConfigurator.MapCommand<ChangeMessages.SkuMustBeIntegrated>();
            EndpointConfigurator.MapCommand<ChangeMessages.IntegrateSku>();

            services
                .AddScoped(provider => provider.CreateRequestClient<ChangeMessages.SkuMustBeIntegrated>(TimeSpan.FromMinutes(1)));

            services
                .AddScoped<ISkuIntegrationService, SkuIntegrationService>();
        }
    }
}
