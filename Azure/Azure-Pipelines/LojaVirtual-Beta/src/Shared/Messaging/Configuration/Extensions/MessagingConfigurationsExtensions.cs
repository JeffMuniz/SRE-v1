using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.Configuration.Hosting;
using System;

namespace Shared.Messaging.Configuration.Extensions
{
    public static class MessagingConfigurationsExtensions
    {
        public static IServiceCollection AddMessaging(
            this IServiceCollection services,
            string connectionString,
            Action<IBusRegistrationContext, IServiceBusBusFactoryConfigurator> busConfig = null
        ) =>
            services
                .AddMessaging(connectionString, null, busConfig);

        public static IServiceCollection AddMessaging(
            this IServiceCollection services,
            string connectionString,
            Action<IServiceCollectionBusConfigurator> configureServices,
            Action<IBusRegistrationContext, IServiceBusBusFactoryConfigurator> busConfig = null
        ) =>
            services
                .AddMassTransit(masstransit =>
                {
                    masstransit.UsingAzureServiceBus((bus, cfg) =>
                    {
                        cfg.Host(connectionString);

                        busConfig?.Invoke(bus, cfg);
                    });
                    configureServices?.Invoke(masstransit);
                })
                .AddHostedService<MessagingHostedService>();
    }
}
