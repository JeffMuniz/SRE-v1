using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Worker.Configurations;

namespace Shared.Worker
{
    public class Startup
    {
        protected readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
        }

        public virtual void Configure(IHostBuilder app, IHostEnvironment env)
        {
        }

        public virtual void Configure(IHost app, IHostEnvironment env)
        {
        }

        internal void InternalConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMappers();

            ConfigureServices(services);
        }

        internal void InternalConfigure(IHostBuilder builder, IHostEnvironment env)
        {
            Configure(builder, env);
        }

        internal void InternalConfigure(IHost app, IHostEnvironment env)
        {
            Configure(app, env);
        }
    }
}
