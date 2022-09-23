using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using System;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Catalog.Integration.Tool
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _ = services
                .AddMessaging(
                    Configuration.GetConnectionString("ServiceBus"),
                    busCollection =>
                    {
                        busCollection.AddRequestClient<ChangeMessages.SkuMustBeIntegrated>(TimeSpan.FromMinutes(1));
                        busCollection.AddRequestClient<ChangeMessages.GetSkuDetail>(TimeSpan.FromMinutes(1));
                        busCollection.AddRequestClient<GetLatestAvailability>(TimeSpan.FromMinutes(1));
                        busCollection.AddRequestClient<GetUnavailableSkus>(TimeSpan.FromMinutes(1));

                        EndpointConfigurator.MapCommand<ChangeMessages.SkuMustBeIntegrated>();
                        EndpointConfigurator.MapCommand<ChangeMessages.IntegrateSku>();
                        EndpointConfigurator.MapCommand<ChangeMessages.GetSkuDetail>();


                        EndpointConfigurator.MapCommand<SagaMessages.Enrichment.UpdateSkuEnriched>();
                        EndpointConfigurator.MapCommand<SagaMessages.Enrichment.SendSkuForEnrichment>();
                        EndpointConfigurator.MapCommand<SagaMessages.Persistence.RemoveSku>();
                        EndpointConfigurator.MapCommand<SagaMessages.Persistence.UpsertSku>();
                        EndpointConfigurator.MapCommand<SagaMessages.Search.SendSkuToSearchIndex>();
                        EndpointConfigurator.MapEvent<SagaMessages.Search.SkuIndexedInTheSearch>();
                        EndpointConfigurator.MapCommand<SagaMessages.Search.RemoveSkuFromSearchIndex>();
                        EndpointConfigurator.MapEvent<SagaMessages.Search.SkuRemovedFromSearchIndex>();
                        EndpointConfigurator.MapCommand<SagaMessages.Categorization.CategorizeSku>();
                        EndpointConfigurator.MapEvent<SagaMessages.Categorization.SkuCategorized>();

                        EndpointConfigurator.MapCommand<AvailabilityMessages.Search.GetAvailability>();
                        EndpointConfigurator.MapCommand<AvailabilityMessages.Search.GetAvailabilityForAllContracts>();
                        EndpointConfigurator.MapEvent<AvailabilityChanged>();

                        EndpointConfigurator.MapCommand<GetLatestAvailability>();
                        EndpointConfigurator.MapCommand<GetUnavailableSkus>();
                        EndpointConfigurator.MapCommand<RemoveSku>();
                        EndpointConfigurator.MapCommand<CheckAvailabilityCacheMustBeRenewed>();
                    }
                )
                .AddControllers()
                .AddNewtonsoftJson(cfg => cfg.UseMemberCasing());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseDefaultFiles();

            app.UseStaticFiles();
        }
    }
}
