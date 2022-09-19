using Shared.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Worker.Configurations
{
    internal static class HostConfig
    {
        public static async Task RunCustomAsync(this IHostBuilder hostBuilder, Action<IHostBuilder> beforeBuilderAction = default, Action<IHost> beforeRunAction = default, CancellationToken cancellationToken = default)
        {
            using var host = hostBuilder
                .ExecuteBeforeBuilder(beforeBuilderAction)
                .Build();

            var logger = host.GetLogger();

            try
            {
                await host
                    .ExecuteStartupConfigureHost()
                    .ExecuteBeforeRun(beforeRunAction ?? ConfigureDefaultHostEvents)
                    .RunAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.RegisterUnhandledError(ex);
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        private static IHostBuilder ExecuteBeforeBuilder(this IHostBuilder builder, Action<IHostBuilder> beforeBuilderAction)
        {
            beforeBuilderAction?.Invoke(builder);
            return builder;
        }

        private static IHost ExecuteStartupConfigureHost(this IHost host)
        {
            var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
            host.Services.GetRequiredService<Startup>().InternalConfigure(host, hostEnvironment);
            return host;
        }

        private static IHost ExecuteBeforeRun(this IHost host, Action<IHost> beforeRunAction)
        {
            beforeRunAction?.Invoke(host);
            return host;
        }

        private static void ConfigureDefaultHostEvents(IHost host)
        {
            var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
            var logger = host.GetLogger();

            var hostApplicationLifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
            hostApplicationLifetime.ApplicationStarted.Register(() =>
                logger.LogWarning($"Iniciando o worker [{hostEnvironment.ApplicationName}]")
            );
            hostApplicationLifetime.ApplicationStopping.Register(() =>
                logger.LogWarning($"Parando o worker [{hostEnvironment.ApplicationName}]")
            );

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    logger.RegisterUnhandledError(ex);
            };
        }

        private static ILogger GetLogger(this IHost host)
        {
            var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
            var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();
            return loggerFactory.CreateLogger(hostEnvironment.ApplicationName);
        }

        private static void RegisterUnhandledError(this ILogger logger, Exception ex) =>
            logger.LogError(ex, $"Erro não tratado");
    }
}
