using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Winton.Extensions.Configuration.Consul;
using ConfigSources = Microsoft.Extensions.Configuration;

namespace Shared.Worker.Configurations
{
    internal static class AppConfigurationsConfig
    {
        public static IHostBuilder ConfigureCustomAppConfigurations(this IHostBuilder builder) =>
            builder
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.WorkarroundSanitizeDuplicatedConfigurationSources();

                    context.Configuration = config.Build();

                    if (!context.Configuration.GetValue("Consul:Enable", false))
                        return;

                    config.AddConsul(
                        context.Configuration.GetValue("Consul:Key", context.HostingEnvironment.ApplicationName),
                        options =>
                        {
                            options.Parser = new Winton.Extensions.Configuration.Consul.Parsers.JsonConfigurationParser();
                            options.ReloadOnChange = true;
                            options.Optional = true;

                            var logger = NLog.LogManager.GetLogger("Consul");

                            options.ConsulConfigurationOptions = consul =>
                            {
                                consul.Address = context.Configuration.GetValue<Uri>("Consul:Address");
                                consul.Token = context.Configuration.GetValue<string>("Consul:Token");
                                consul.Datacenter = context.Configuration.GetValue<string>("Consul:Datacenter");
                            };
                            options.OnLoadException = ctx =>
                            {
                                logger.Error(ctx.Exception, "Consul - OnLoadException - Key: {Key}", ctx.Source?.Key);
                            };
                            options.OnWatchException = ctx =>
                            {
                                logger.Warn(ctx.Exception, "Consul - OnWatchException - Key: {Key}, ConsecutiveFailureCount: {ConsecutiveFailureCount}", ctx.Source?.Key, ctx.ConsecutiveFailureCount);
                                return TimeSpan.FromSeconds(5);
                            };
                        }
                    );

                    config.WorkarroundAdjustPrioritiesConfigurationSources();
                });

        private static void WorkarroundSanitizeDuplicatedConfigurationSources(this IConfigurationBuilder config)
        {
            var duplicateds = config.Sources
                .GroupBy(x => x.GetType())
                .Where(x => x.Count() > 1)
                .SelectMany(x => x.AsEnumerable())
                .ToArray();

            void removeDuplicated<TSource, TKey>(
                IEnumerable<IConfigurationSource> sources,
                Func<TSource, TKey> keySelector
            ) where TSource : IConfigurationSource =>
                sources
                    .OfType<TSource>()
                    .GroupBy(keySelector)
                    .Where(x => x.Count() > 1)
                    .SelectMany(x => x.AsEnumerable())
                    .Skip(1)
                    .AsParallel()
                    .WithDegreeOfParallelism(1)
                    .ForAll(x => config.Sources.Remove(x));

            removeDuplicated<ConfigSources.Json.JsonConfigurationSource, string>(duplicateds, x => x.Path);
            removeDuplicated<ConfigSources.EnvironmentVariables.EnvironmentVariablesConfigurationSource, string>(duplicateds, x => x.Prefix);
        }

        private static void WorkarroundAdjustPrioritiesConfigurationSources(this IConfigurationBuilder config)
        {
            void moveToLast<TSource>() where TSource : IConfigurationSource
            {
                var adjustSource = config.Sources.OfType<TSource>().FirstOrDefault();
                if (adjustSource is null ||
                    adjustSource.Equals(config.Sources.LastOrDefault()))
                    return;

                config.Sources.Remove(adjustSource);
                config.Sources.Add(adjustSource);
            }

            moveToLast<ConfigSources.EnvironmentVariables.EnvironmentVariablesConfigurationSource>();
            moveToLast<ConfigSources.CommandLine.CommandLineConfigurationSource>();
        }
    }
}
