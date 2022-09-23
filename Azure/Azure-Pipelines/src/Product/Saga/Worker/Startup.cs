using GreenPipes;
using MassTransit;
using MassTransit.MongoDbIntegration.Saga.CollectionNameFormatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Product.Saga.Worker.Backend.Infrastructure.Persistence.Contexts;
using Product.Saga.Worker.Backend.Infrastructure.Persistence.Repositories;
using Product.Saga.Worker.Configurations;
using Product.Saga.Worker.Saga;
using Product.Saga.Worker.Saga.States;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using Shared.Persistence.Mongo.Configurations;
using Shared.Persistence.Mongo.Extensions;
using System;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;
using BaseWorker = Shared.Worker;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Product.Saga.Worker
{
    public class Startup : BaseWorker.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMongoContext<IntegrationContext, MongoOptions>(
                    _configuration.GetSection("Mongo:Integration"),
                    options =>
                    {
                        options.ApplicationName = _configuration.GetValue<string>("Mongo:ApplicationName");
                        options.ConnectionString = _configuration.GetConnectionString("Integration");
                    }
                 )
                .AddSingleton<IIntegrationContext, IntegrationContext>()
                .AddSingleton<ISkuSagaHistoryRepository, SkuSagaHistoryRepository>();

            services
                .AddMessaging(
                    connectionString: _configuration.GetConnectionString("ServiceBus"),
                    configureServices: masstransit =>
                    {
                        masstransit
                            .AddServiceBusMessageScheduler();

                        masstransit.Collection
                            .AddSingleton<BsonClassMap<SkuState>, Saga.States.Mappings.SkuStateMap>()
                            .AddSingleton<BsonClassMap<Shared.Messaging.Contracts.Shared.Models.SupplierSku>, Saga.States.Mappings.SupplierSkuMap>()
                            .AddSingleton<BsonClassMap<Shared.Messaging.Contracts.Shared.Models.Price>, Saga.States.Mappings.PriceMap>();

                        masstransit
                            .AddSagaStateMachine<SkuSaga, SkuState>()
                            .MongoDbRepository(repository =>
                            {
                                repository.DatabaseName = "None";
                                repository.DatabaseFactory(provider =>
                                    provider.GetRequiredService<IIntegrationContext>().Database
                                );
                                repository.CollectionNameFormatter(provider =>
                                {
                                    var mongoOptions = provider.GetRequiredService<IIntegrationContext>().Options;
                                    return new DefaultCollectionNameFormatter($"{mongoOptions.CollectionPrefix}sku");
                                });
                            });
                    },
                    busConfig: (provider, config) =>
                    {
                        config.UseServiceBusMessageScheduler();
                        config.UseMessageRetry(retry =>
                            retry.Interval(1, TimeSpan.FromSeconds(5))
                        );

                        EndpointConfigurator.MapCommand<SagaMessages.Categorization.CategorizeSku>();
                        EndpointConfigurator.MapCommand<SagaMessages.Persistence.UpsertSku>();
                        EndpointConfigurator.MapCommand<SagaMessages.Search.SendSkuToSearchIndex>();
                        EndpointConfigurator.MapCommand<AvailabilityMessages.Search.GetAvailabilityForAllContracts>();
                        EndpointConfigurator.MapCommand<SagaMessages.Persistence.RemoveSku>();
                        EndpointConfigurator.MapCommand<AvailabilityMessages.Manager.RemoveSku>();
                        EndpointConfigurator.MapCommand<SagaMessages.Search.RemoveSkuFromSearchIndex>();
                        EndpointConfigurator.MapCommand<SagaMessages.Enrichment.SendSkuForEnrichment>();

                        config
                            .ConfigureSagaCommand<SkuState, SagaMessages.Change.AddSku>(provider)
                            .ConfigureSagaCommand<SkuState, SagaMessages.Change.UpdateSku>(provider)
                            .ConfigureSagaCommand<SkuState, SagaMessages.Change.RemoveSku>(provider)
                            .ConfigureSagaCommand<SkuState, SagaMessages.Enrichment.UpdateSkuEnriched>(provider)
                            .ConfigureSagaEvent<SkuState, SagaMessages.Categorization.SkuCategorized>(provider)
                            .ConfigureSagaEvent<SkuState, SagaMessages.Persistence.SkuUpserted>(provider)
                            .ConfigureSagaEvent<SkuState, SagaMessages.Search.SkuIndexedInTheSearch>(provider)
                            .ConfigureSagaEvent<SkuState, SagaMessages.Persistence.SkuRemoved>(provider)
                            .ConfigureSagaEvent<SkuState, AvailabilityMessages.Manager.SkuRemovedFromAvailability>(provider)
                            .ConfigureSagaEvent<SkuState, SagaMessages.Search.SkuRemovedFromSearchIndex>(provider);
                    }
                );
        }
    }
}
