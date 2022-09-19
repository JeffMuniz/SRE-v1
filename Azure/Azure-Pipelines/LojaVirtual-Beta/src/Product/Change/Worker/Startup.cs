using GreenPipes;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Change.Worker.Backend.Application.Usecases.GetSkuDetail;
using Product.Change.Worker.Backend.Application.Usecases.IntegrateSku;
using Product.Change.Worker.Backend.Application.Usecases.Shared.Configurations;
using Product.Change.Worker.Backend.Application.Usecases.SkuMustBeIntegrated;
using Product.Change.Worker.Backend.Domain.Repositories;
using Product.Change.Worker.Backend.Domain.Services;
using Product.Change.Worker.Backend.Infrastructure.Hash;
using Product.Change.Worker.Backend.Infrastructure.Messaging;
using Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Context;
using Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Repositories;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using Shared.Persistence.Mongo.Extensions;
using System;
using BaseWorker = Shared.Worker;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Product.Change.Worker
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
                 .AddMessaging(
                     _configuration.GetConnectionString("ServiceBus"),
                     bus =>
                     {
                         bus.AddConsumer<Consumers.SkuMustBeIntegratedConsumer>();
                         bus.AddConsumer<Consumers.IntegrateSkuConsumer>();
                         bus.AddConsumer<Consumers.GetSkuDetailConsumer>();
                     },
                     (provider, config) =>
                     {
                         config.UseMessageRetry(retry =>
                            retry.Interval(1, TimeSpan.FromSeconds(5))
                         );

                         EndpointConfigurator.MapCommand<ChangeMessages.SkuMustBeIntegrated>();
                         EndpointConfigurator.MapCommand<ChangeMessages.IntegrateSku>();
                         EndpointConfigurator.MapCommand<ChangeMessages.GetSkuDetail>();

                         EndpointConfigurator.MapCommand<SagaMessages.Change.AddSku>();
                         EndpointConfigurator.MapCommand<SagaMessages.Change.UpdateSku>();
                         EndpointConfigurator.MapCommand<SagaMessages.Change.RemoveSku>();

                         EndpointConfigurator.MapEvent<ChangeMessages.SkuPriceChanged>();

                         config.ReceiveEndpoint(EndpointConfigurator.GetEndpointName<ChangeMessages.SkuMustBeIntegrated>(), endpoint =>
                         {                             
                             endpoint.PrefetchCount = 20;
                             endpoint.MaxConcurrentCalls = 10;
                             endpoint.UseInMemoryOutbox();
                             endpoint.ConfigureDeadLetterQueueDeadLetterTransport();
                             endpoint.ConfigureDeadLetterQueueErrorTransport();
                             endpoint.ConfigureConsumeTopology = false;
                             endpoint.MaxAutoRenewDuration = TimeSpan.FromMinutes(1);
                             endpoint.Consumer<Consumers.SkuMustBeIntegratedConsumer>(provider);
                         });

                         config.ReceiveEndpoint(EndpointConfigurator.GetEndpointName<ChangeMessages.IntegrateSku>(), endpoint =>
                         {                             
                             endpoint.PrefetchCount = 20;
                             endpoint.MaxConcurrentCalls = 10;
                             endpoint.UseInMemoryOutbox();
                             endpoint.ConfigureDeadLetterQueueDeadLetterTransport();
                             endpoint.ConfigureDeadLetterQueueErrorTransport();
                             endpoint.ConfigureConsumeTopology = false;
                             endpoint.MaxAutoRenewDuration = TimeSpan.FromMinutes(1);
                             endpoint.Consumer<Consumers.IntegrateSkuConsumer>(provider);
                         });

                         config.ReceiveEndpoint(EndpointConfigurator.GetEndpointName<ChangeMessages.GetSkuDetail>(), endpoint =>
                         {                             
                             endpoint.PrefetchCount = 20;
                             endpoint.MaxConcurrentCalls = 10;
                             endpoint.UseInMemoryOutbox();
                             endpoint.ConfigureDeadLetterQueueDeadLetterTransport();
                             endpoint.ConfigureDeadLetterQueueErrorTransport();
                             endpoint.ConfigureConsumeTopology = false;
                             endpoint.MaxAutoRenewDuration = TimeSpan.FromMinutes(1);
                             endpoint.Consumer<Consumers.GetSkuDetailConsumer>(provider);
                         });
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
                .Configure<IntegrationSkuConfigurationOptions>(_configuration.GetSection("IntegrationSku"));

            services
                .AddScoped<ICrcHashProviderService, CrcHashProviderService>()
                .AddScoped<ISkuNotificationService, SkuNotificationService>()
                .AddScoped<ISkuIntegrationRepository, SkuIntegrationRepository>()
                .AddScoped<ISkuMustBeIntegratedUsecase, SkuMustBeIntegratedUsecase>()
                .AddScoped<IIntegrateSkuUsecase, IntegrateSkuUsecase>()
                .AddScoped<IGetSkuDetailUsecase, GetSkuDetailUsecase>();
        }
    }
}
