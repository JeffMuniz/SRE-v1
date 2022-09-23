using Automatonymous;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Shared.Messaging.Configuration;
using System;

namespace Product.Saga.Worker.Configurations
{
    public static class ProductSagaConfigurationExtensions
    {
        private static readonly string AppSubscriptionName = typeof(Program).Namespace;

        public static IServiceBusBusFactoryConfigurator ConfigureSagaCommand<TSagaInstance, TCommand>(
            this IServiceBusBusFactoryConfigurator config,
            IBusRegistrationContext provider,
            Action<IServiceBusReceiveEndpointConfigurator> custom = null
        )
            where TSagaInstance : class, SagaStateMachineInstance
            where TCommand : class
        {
            EndpointConfigurator.MapCommand<TCommand>();

            config.ReceiveEndpoint(
                EndpointConfigurator.GetEndpointName<TCommand>(),
                endpoint =>
                {
                    endpoint.ConfigureConsumeTopology = false;
                    endpoint.PrefetchCount = 1;
                    endpoint.MaxConcurrentCalls = 1;
                    endpoint.MaxAutoRenewDuration = TimeSpan.FromMinutes(1);
                    endpoint.UseInMemoryOutbox();
                    endpoint.ConfigureDeadLetterQueueDeadLetterTransport();
                    endpoint.ConfigureDeadLetterQueueErrorTransport();
                    endpoint.StateMachineSaga<TSagaInstance>(provider);

                    custom?.Invoke(endpoint);
                }
            );

            return config;
        }

        public static IServiceBusBusFactoryConfigurator ConfigureSagaEvent<TSagaInstance, TEvent>(
            this IServiceBusBusFactoryConfigurator config,
            IBusRegistrationContext provider,
            Action<IServiceBusSubscriptionEndpointConfigurator> custom = null
        )
            where TSagaInstance : class, SagaStateMachineInstance
            where TEvent : class
        {
            EndpointConfigurator.MapEvent<TEvent>();

            config.SubscriptionEndpoint<TEvent>(
                AppSubscriptionName,
                endpoint =>
                {
                    endpoint.ConfigureConsumeTopology = false;
                    endpoint.PrefetchCount = 1;
                    endpoint.MaxConcurrentCalls = 1;
                    endpoint.MaxAutoRenewDuration = TimeSpan.FromMinutes(1);
                    endpoint.UseInMemoryOutbox();
                    endpoint.ConfigureDeadLetterQueueDeadLetterTransport();
                    endpoint.ConfigureDeadLetterQueueErrorTransport();
                    endpoint.StateMachineSaga<TSagaInstance>(provider);

                    custom?.Invoke(endpoint);
                }
            );

            return config;
        }
    }
}
