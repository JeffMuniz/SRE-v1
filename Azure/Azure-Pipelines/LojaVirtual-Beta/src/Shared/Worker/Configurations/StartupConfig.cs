using Shared.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Shared.Worker.Configurations
{
    public static class StartupConfig
    {
        public static IHostBuilder UseStartup<TStartup>(this IHostBuilder app) where TStartup : Startup
        {
            return app
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddSingleton(hostingContext.Configuration);

                    var startupCtor = typeof(TStartup).GetTypeInfo().GetConstructor(new[] { typeof(IConfiguration) });
                    if (startupCtor == null)
                        return;

                    var startup = startupCtor.Invoke(new[] { hostingContext.Configuration }) as TStartup;
                    services.AddSingleton<Startup>(startup);
                    startup.InternalConfigureServices(services);
                    startup.InternalConfigure(app, hostingContext.HostingEnvironment);
                });
        }
    }
}
