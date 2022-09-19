using Shared.Worker.Configurations;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Worker
{
    public abstract class Program<TStartup>
        where TStartup : Startup
    {
        protected Program()
        {
        }

        public static Task Run(string[] args) =>
            Run(args, CancellationToken.None);

        public static Task Run(string[] args, CancellationToken cancellationToken) =>
            Run(args, beforeBuilderAction: default, beforeRunAction: default, cancellationToken: cancellationToken);

        public static Task Run(string[] args, Action<IHostBuilder> beforeBuilderAction = default, Action<IHost> beforeRunAction = default, CancellationToken cancellationToken = default) =>
            CreateHostBuilder(args)
                .RunCustomAsync(beforeBuilderAction, beforeRunAction, cancellationToken);

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebJobs(builder => builder
                    .AddAzureStorageCoreServices()
                )
                .ConfigureCustomLogging()
                .ConfigureCustomAppConfigurations()
                .UseStartup<TStartup>();
    }
}
