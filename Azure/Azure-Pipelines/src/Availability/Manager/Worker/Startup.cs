using Availability.Manager.Worker.Backend.Domain.Repositories;
using Availability.Manager.Worker.Backend.Domain.Services;
using Availability.Manager.Worker.Backend.Infrastructure.Messaging;
using Availability.Manager.Worker.Backend.Infrastructure.Persistence.Contexts;
using Availability.Manager.Worker.Backend.Infrastructure.Persistence.Repositories;
using Availability.Manager.Worker.Configurations;
using Availability.Manager.Worker.Consumers.AvailabilityFound;
using Availability.Manager.Worker.Consumers.CheckAvailabilityCacheMustBeRenewed;
using Availability.Manager.Worker.Consumers.GetLatestAvailability;
using Availability.Manager.Worker.Consumers.GetUnavailableSkus;
using Availability.Manager.Worker.Consumers.RemoveSku;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using Shared.Messaging.Contracts.Availability.Messages.Search;
using Shared.Persistence.Mongo.Extensions;
using System;
using BaseWorker = Shared.Worker;
using Usecases = Availability.Manager.Worker.Backend.Application.UseCases;

namespace Availability.Manager.Worker
{
    public class Startup : BaseWorker.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var subscriptionClientName = EndpointConfigurator.GetApplicationSubscriptionName<Program>();

            services
                 .AddMessaging(
                     _configuration.GetConnectionString("ServiceBus"),
                     masstransit =>
                     {
                         masstransit.AddServiceBusMessageScheduler();
                         masstransit.Collection.AddScoped(provider =>
                         {
                             var bus = provider.GetRequiredService<IBus>();
                             return bus.CreateServiceBusMessageScheduler(bus.Topology);
                         });

                         masstransit.AddConsumer<AvailabilityFoundConsumer>();
                         masstransit.AddConsumer<CheckAvailabilityCacheMustBeRenewedConsumer>();
                         masstransit.AddConsumer<GetLatestAvailabilityConsumer>();
                         masstransit.AddConsumer<GetUnavailableSkusConsumer>();
                         masstransit.AddConsumer<RemoveSkuConsumer>();
                     },
                     (provider, config) =>
                     {
                         config.UseServiceBusMessageScheduler();
                         config.UseMessageRetry(retry =>
                            retry.Interval(2, TimeSpan.FromSeconds(5))
                         );

                         EndpointConfigurator.MapCommand<CheckAvailabilityCacheMustBeRenewed>();
                         EndpointConfigurator.MapCommand<GetAvailability>();
                         EndpointConfigurator.MapCommand<GetLatestAvailability>();
                         EndpointConfigurator.MapCommand<GetUnavailableSkus>();
                         EndpointConfigurator.MapCommand<RemoveSku>();

                         EndpointConfigurator.MapEvent<AvailabilityFound>();
                         EndpointConfigurator.MapEvent<AvailabilityChanged>();
                         EndpointConfigurator.MapEvent<SkuRemovedFromAvailability>();

                         config.ConfigureSubscription<AvailabilityFound, AvailabilityFoundConsumer>(provider);
                         config.ConfigureReceive<CheckAvailabilityCacheMustBeRenewed, CheckAvailabilityCacheMustBeRenewedConsumer>(provider);
                         config.ConfigureReceive<GetLatestAvailability, GetLatestAvailabilityConsumer>(provider);
                         config.ConfigureReceive<GetUnavailableSkus, GetUnavailableSkusConsumer>(provider);
                         config.ConfigureReceive<RemoveSku, RemoveSkuConsumer>(provider,
                             configure: endpoint =>
                             {
                                 endpoint.UseDelayedRedelivery(r =>
                                 {
                                     r.Handle<System.Net.Http.HttpRequestException>(ex =>
                                         ex.StatusCode != System.Net.HttpStatusCode.NotFound
                                     );
                                     r.Intervals(
                                        TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(50),
                                        TimeSpan.FromHours(2), TimeSpan.FromHours(4), TimeSpan.FromHours(10),
                                        TimeSpan.FromDays(1), TimeSpan.FromDays(2)
                                    );
                                 });
                             }
                        );
                     }
                 );

            services
                .AddMongoContext<IntegrationContext>(
                    _configuration.GetSection("Mongo:Integration"),
                    options =>
                    {
                        options.ApplicationName = _configuration.GetValue<string>("Mongo:ApplicationName");
                        options.ConnectionString = _configuration.GetConnectionString("Integration");
                    }
                 )
                .AddSingleton<IIntegrationContext, IntegrationContext>();

            services
                .AddRedis(
                    _configuration.GetSection("RedisConfiguration")
                )
                .AddCache(
                    _configuration.GetSection("CacheConfiguration")
                );

            services
                .Configure<Configurations.Models.SearchConfigurationOptions>(
                    _configuration.GetSection("SearchConfiguration")
                );

            services
                .AddScoped<IAvailabilityNotificationService, AvailabilityNotificationService>()
                .AddScoped<ICacheRenewScheduleService, CacheRenewScheduleService>()
                .AddScoped<ISkuRepository, SkuRepository>()
                .AddScoped<ICacheRenewScheduleRepository, CacheRenewScheduleRepository>()
                .AddScoped<Usecases.AvailabilityFound.IAvailabilityFoundUseCase, Usecases.AvailabilityFound.AvailabilityFoundUseCase>()
                .AddScoped<Usecases.CheckAvailabilityCacheMustBeRenewed.ICheckAvailabilityCacheMustBeRenewedUseCase, Usecases.CheckAvailabilityCacheMustBeRenewed.CheckAvailabilityCacheMustBeRenewedUseCase>()
                .AddScoped<Usecases.GetLatestAvailability.IGetLatestAvailabilityUseCase, Usecases.GetLatestAvailability.GetLatestAvailabilityUseCase>()
                .AddScoped<Usecases.GetUnavailableSkus.IGetUnavailableSkusUseCase, Usecases.GetUnavailableSkus.GetUnavailableSkusUseCase>()
                .AddScoped<Usecases.RemoveSku.IRemoveSkuUseCase, Usecases.RemoveSku.RemoveSkuUseCase>();
        }
    }
}
