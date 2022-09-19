using GreenPipes;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Product.Enrichment.macnaima.Worker.Backend.Application.Usecases.NotifyPendingOffer;
using Product.Enrichment.macnaima.Worker.Backend.Domain.Services;
using Product.Enrichment.macnaima.Worker.Backend.Infrastructure.ExternalServices;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using System;
using System.Net.Http.Headers;
using BaseWorker = Shared.Worker;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Product.Enrichment.macnaima.Worker
{
    public class Startup : BaseWorker.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        { }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMessaging(
                    _configuration.GetConnectionString("ServiceBus"),
                    bus =>
                    {
                        bus.AddConsumer<Consumers.SendSkuForEnrichmentConsumer>();
                    },
                    (provider, config) =>
                    {
                        config.UseMessageRetry(retry =>
                            retry.Interval(1, TimeSpan.FromSeconds(5))
                         );

                        EndpointConfigurator.MapCommand<SagaMessages.Enrichment.SendSkuForEnrichment>();

                        config.ReceiveEndpoint(EndpointConfigurator.GetEndpointName<SagaMessages.Enrichment.SendSkuForEnrichment>(),
                            endpoint =>
                            {
                                endpoint.ConfigureConsumeTopology = false;
                                endpoint.PrefetchCount = 1;
                                endpoint.MaxAutoRenewDuration = TimeSpan.FromMinutes(1);
                                endpoint.UseInMemoryOutbox();
                                endpoint.ConfigureDeadLetterQueueDeadLetterTransport();
                                endpoint.ConfigureDeadLetterQueueErrorTransport();                                                                
                                endpoint.Consumer<Consumers.SendSkuForEnrichmentConsumer>(provider);
                            });
                    }
                );

            services
                .Configure<MacnaimaConfigurationOptions>(_configuration.GetSection("macnaima"))
                .AddHttpClient<IMacnaimaService, MacnaimaService>((provider, client) =>
                {
                    var settings = provider.GetRequiredService<IOptionsMonitor<MacnaimaConfigurationOptions>>();
                    client.BaseAddress = settings.CurrentValue.BaseAddress;
                    client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(settings.CurrentValue.AccessKey);
                });

            services
                .AddScoped<INotifyPendingOfferUsecase, NotifyPendingOfferUsecase>();
        }
    }
}
