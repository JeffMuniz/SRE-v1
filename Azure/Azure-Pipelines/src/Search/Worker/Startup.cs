using GreenPipes;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Search.Worker.Backend.Infrastructure.Persistence;
using Search.Worker.Backend.Infrastructure.Persistence.Models;
using Search.Worker.Consumers.AvailabilityChanged;
using Search.Worker.Consumers.RemoveSkuFromSearchIndex;
using Search.Worker.Consumers.SendSkuToSearchIndex;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using SolrNet;
using System;
using AvailabilityMessagingContracts = Shared.Messaging.Contracts.Availability;
using BaseWorker = Shared.Worker;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;
using UseCases = Search.Worker.Backend.Application.Usecases;

namespace Search.Worker
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
                .AddSolrNet<SearchIndexModel>(_configuration.GetSection("Solr:Url").Value)
                .AddScoped<Backend.Domain.Services.ISearchIndexRepository, SearchIndexRepository>();

            services
                .AddSingleton<Backend.Domain.Services.IHashProviderService, Backend.Infrastructure.Hash.Base64HashProviderService>();

            services
                .AddScoped<UseCases.UpsertSku.IUpsertSkuUsecase, UseCases.UpsertSku.UpsertSkuUsecase>()
                .AddScoped<UseCases.UpdateSkuAvailability.IUpdateSkuAvailabilityUsecase, UseCases.UpdateSkuAvailability.UpdateSkuAvailabilityUsecase>()
                .AddScoped<UseCases.RemoveSku.IRemoveSkuUsecase, UseCases.RemoveSku.RemoveSkuUsecase>();

            services
                .AddMessaging(
                    _configuration.GetConnectionString("ServiceBus"),
                    masstransit =>
                    {
                        masstransit.AddConsumer<SendSkuToSearchIndexConsumer>();
                        masstransit.AddConsumer<RemoveSkuFromSearchIndexConsumer>();
                        masstransit.AddConsumer<AvailabilityChangedConsumer>();
                    },
                    (provider, config) =>
                    {
                        config.UseMessageRetry(retry =>
                            retry.Interval(2, TimeSpan.FromSeconds(5))
                        );

                        EndpointConfigurator.MapCommand<SagaMessages.Search.SendSkuToSearchIndex>();
                        EndpointConfigurator.MapEvent<SagaMessages.Search.SkuIndexedInTheSearch>();
                        EndpointConfigurator.MapCommand<SagaMessages.Search.RemoveSkuFromSearchIndex>();
                        EndpointConfigurator.MapEvent<SagaMessages.Search.SkuRemovedFromSearchIndex>();
                        EndpointConfigurator.MapEvent<AvailabilityMessagingContracts.Messages.Manager.AvailabilityChanged>();

                        config.ConfigureReceive<SagaMessages.Search.SendSkuToSearchIndex, SendSkuToSearchIndexConsumer>(provider, configureDelayedRedelivery);
                        config.ConfigureReceive<SagaMessages.Search.RemoveSkuFromSearchIndex, RemoveSkuFromSearchIndexConsumer>(provider, configureDelayedRedelivery);
                        config.ConfigureSubscription<AvailabilityMessagingContracts.Messages.Manager.AvailabilityChanged, AvailabilityChangedConsumer>(provider);
                    }
                );
        }
    }
}
