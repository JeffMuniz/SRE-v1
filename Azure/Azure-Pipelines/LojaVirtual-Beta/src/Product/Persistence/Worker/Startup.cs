using GreenPipes;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Product.Persistence.Worker.Backend.Domain.Services;
using Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence;
using Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Configurations;
using Product.Persistence.Worker.Consumers.AvailabilityChanged;
using Product.Persistence.Worker.Consumers.RemoveSku;
using Product.Persistence.Worker.Consumers.UpsertSku;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using Shared.Messaging.Contracts.Product.Saga.Messages.Persistence;
using System;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using BaseWorker = Shared.Worker;
using Usecases = Product.Persistence.Worker.Backend.Application.Usecases;

namespace Product.Persistence.Worker
{
    public class Startup : BaseWorker.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            static void configureDelayedRedelivery(IServiceBusReceiveEndpointConfigurator x)
            {
                x.UseDelayedRedelivery(r =>
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
            };

            services
                .AddMemoryCache();

            services
                 .AddMessaging(
                     _configuration.GetConnectionString("ServiceBus"),
                     masstransit =>
                     {
                         masstransit.AddConsumer<RemoveSkuConsumer>();
                         masstransit.AddConsumer<UpsertSkuConsumer>();
                         masstransit.AddConsumer<AvailabilityChangedConsumer>();
                     },
                     (provider, config) =>
                     {
                         config.UseMessageRetry(retry =>
                            retry.Interval(2, TimeSpan.FromSeconds(5))
                         );

                         EndpointConfigurator.MapCommand<RemoveSku>();
                         EndpointConfigurator.MapEvent<SkuRemoved>();
                         EndpointConfigurator.MapCommand<UpsertSku>();
                         EndpointConfigurator.MapEvent<SkuUpserted>();
                         EndpointConfigurator.MapEvent<AvailabilityMessagingContracts.Messages.Manager.AvailabilityChanged>();

                         config.ConfigureReceive<UpsertSku, UpsertSkuConsumer>(provider,
                             endpoint =>
                             {
                                 var concurrentMessageLimit = _configuration.GetValue("Consumers:UpsertSku:ConcurrentMessageLimit", 1);
                                 endpoint.PrefetchCount = concurrentMessageLimit * 2;
                                 endpoint.ConcurrentMessageLimit = concurrentMessageLimit;
                                 configureDelayedRedelivery(endpoint);
                             });
                         config.ConfigureReceive<RemoveSku, RemoveSkuConsumer>(provider,
                             endpoint =>
                             {
                                 var concurrentMessageLimit = _configuration.GetValue("Consumers:RemoveSku:ConcurrentMessageLimit", 1);
                                 endpoint.PrefetchCount = concurrentMessageLimit * 2;
                                 endpoint.ConcurrentMessageLimit = concurrentMessageLimit;
                                 configureDelayedRedelivery(endpoint);
                             });
                         config.ConfigureSubscription<AvailabilityMessagingContracts.Messages.Manager.AvailabilityChanged, AvailabilityChangedConsumer>(provider,
                             configure: endpoint =>
                             {
                                 var concurrentMessageLimit = _configuration.GetValue("Consumers:AvailabilityChanged:ConcurrentMessageLimit", 1);
                                 endpoint.PrefetchCount = concurrentMessageLimit * 2;
                                 endpoint.ConcurrentMessageLimit = concurrentMessageLimit;
                             });
                     }
                 );

            services
                .Configure<StorageClientConfiguration>(_configuration.GetSection("Legacy:Client"))
                .AddHttpClient<IProductStorageService, ProductStorageService>()
                    .ConfigureHttpClient((provider, httpClient) =>
                    {
                        var options = provider.GetRequiredService<IOptionsMonitor<StorageClientConfiguration>>();
                        httpClient.BaseAddress = options.CurrentValue.BaseAddress;
                        httpClient.Timeout = options.CurrentValue.Timeout;
                    });

            services
                .AddSingleton<Shared.Keywords.IKeywordsGenerator, Shared.Keywords.KeywordsGenerator>()
                .AddScoped<Usecases.UpsertSku.IUpsertSkuUseCase, Usecases.UpsertSku.UpsertSkuUseCase>()
                .AddScoped<Usecases.RemoveSku.IRemoveSkuUsecase, Usecases.RemoveSku.RemoveSkuUsecase>()
                .AddScoped<Usecases.UpdateAvailability.IUpdateAvailabilityUsecase, Usecases.UpdateAvailability.UpdateAvailabilityUsecase>();
        }
    }
}
