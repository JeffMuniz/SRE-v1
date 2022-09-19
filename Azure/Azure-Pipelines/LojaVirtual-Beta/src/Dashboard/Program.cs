using Dashboard.Web.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Dashboard.Web
{
    public class Program
    {
        public static async Task Main(string[] args) =>
            await CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder => builder
                    .UseStartup<Startup>()
                    .UseShutdownTimeout(TimeSpan.FromSeconds(30))
                )
                .ConfigureCustomLogging()
                .ConfigureCustomAppConfigurations();
    }
}
