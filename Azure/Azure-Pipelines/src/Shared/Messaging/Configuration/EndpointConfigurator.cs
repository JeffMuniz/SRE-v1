using Humanizer;
using System;
using System.Linq;
using System.Reflection;

namespace Shared.Messaging.Configuration
{
    public static class EndpointConfigurator
    {
        public static Uri GetCommandEndpoint<TMessage>(bool withNamespace = true)
            where TMessage : class
        {
            var name = GetEndpointName<TMessage>(withNamespace);
            return new Uri($"queue:{name}");
        }

        public static Uri GetEventEndpoint<TMessage>(bool withNamespace = true)
            where TMessage : class
        {
            var name = GetEndpointName<TMessage>(withNamespace);
            return new Uri($"topic:{name}");
        }

        public static string GetEndpointName<TMessage>(bool withNamespace = true)
            where TMessage : class
        {
            var prefix = Environment.GetEnvironmentVariable("Messaging__EndpointName__Prefix") ?? "Catalog.Integration";
            var @namespace = withNamespace
                ? typeof(TMessage).Namespace.Replace("Shared.Messaging.Contracts.", "").Replace(".Messages", "").Replace(".", "/")
                : null;
            var entity = typeof(TMessage).Name.Kebaberize();
            var args = new[] { prefix, @namespace, entity }.Where(arg => !string.IsNullOrWhiteSpace(arg)).ToArray();
            var path = string.Join("/", args);

            return path;
        }

        public static string GetApplicationSubscriptionName() =>
            Assembly.GetEntryAssembly().GetTypes().FirstOrDefault(x => "Program".Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))?.Namespace
            ?? throw new NullReferenceException("Program class not found");

        public static string GetApplicationSubscriptionName<TProgram>() where TProgram : class =>
            typeof(TProgram).Namespace;

        public static void MapCommand<T>(bool withNamespace = true) where T : class =>
            MassTransit.EndpointConvention.Map<T>(GetCommandEndpoint<T>(withNamespace));

        public static void MapEvent<T>(bool withNamespace = true) where T : class =>
            MassTransit.EndpointConvention.Map<T>(GetEventEndpoint<T>(withNamespace));
    }
}
