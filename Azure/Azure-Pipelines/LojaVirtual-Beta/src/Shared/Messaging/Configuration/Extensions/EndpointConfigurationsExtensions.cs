using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using System;

namespace Shared.Messaging.Configuration.Extensions
{
    public static class EndpointConfigurationsExtensions
    {
        public static void ConfigureReceive<TMessage, TConsumer>(
            this IServiceBusBusFactoryConfigurator config,
            IBusRegistrationContext provider,
            Action<IServiceBusReceiveEndpointConfigurator> configure = default
        )
            where TMessage : class
            where TConsumer : class, IConsumer<TMessage>
        {
            config.ReceiveEndpoint(
                EndpointConfigurator.GetEndpointName<TMessage>(),
                endpoint => endpoint.ConfigureReceive<TConsumer>(provider, configure)
            );
        }

        public static void ConfigureSubscription<TMessage, TConsumer>(
            this IServiceBusBusFactoryConfigurator config,
            IBusRegistrationContext provider,
            string subscriptionName = default,
            Action<IServiceBusSubscriptionEndpointConfigurator> configure = default
        )
            where TMessage : class
            where TConsumer : class, IConsumer<TMessage>
        {
            config.SubscriptionEndpoint<TMessage>(
                subscriptionName ?? EndpointConfigurator.GetApplicationSubscriptionName(),
                endpoint => endpoint.ConfigureSubscription<TConsumer>(provider, configure)
            );
        }

        public static void ConfigureReceive<TConsumer>(
            this IServiceBusReceiveEndpointConfigurator endpoint,
            IBusRegistrationContext provider,
            Action<IServiceBusReceiveEndpointConfigurator> configure = default
        )
            where TConsumer : class, IConsumer
        {
            endpoint.ConfigureEndpoint<IServiceBusReceiveEndpointConfigurator, TConsumer>(provider, configure);
        }

        public static void ConfigureSubscription<TConsumer>(
            this IServiceBusSubscriptionEndpointConfigurator endpoint,
            IBusRegistrationContext provider,
            Action<IServiceBusSubscriptionEndpointConfigurator> configure = default
        )
            where TConsumer : class, IConsumer
        {
            endpoint.ConfigureEndpoint<IServiceBusSubscriptionEndpointConfigurator, TConsumer>(provider, configure);
        }

        private static void ConfigureEndpoint<TEndpoint, TConsumer>(
            this TEndpoint endpoint,
            IBusRegistrationContext provider,
            Action<TEndpoint> configure = default
        )
            where TEndpoint : IReceiveEndpointConfigurator, IServiceBusEndpointConfigurator
            where TConsumer : class, IConsumer
        {
            endpoint.ConfigureConsumeTopology = false;
            endpoint.PrefetchCount = 1;
            endpoint.MaxAutoRenewDuration = TimeSpan.FromMinutes(1);
            endpoint.UseInMemoryOutbox();
            endpoint.ConfigureDeadLetterQueueDeadLetterTransport();
            endpoint.ConfigureDeadLetterQueueErrorTransport();
            endpoint.Consumer<TConsumer>(provider);

            configure?.Invoke(endpoint);
        }
    }
}
