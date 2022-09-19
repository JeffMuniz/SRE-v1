using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Availability.Api.Configurations
{
    internal static class LoggingConfig
    {
        public static IHostBuilder ConfigureCustomLogging(this IHostBuilder builder) =>
            builder.ConfigureLogging((context, logging) =>
            {
                logging
                    .ClearProviders()
                    .AddConfiguration(context.Configuration.GetSection("Logging"))
                    .AddNLog(context.Configuration);
            });
    }
}
