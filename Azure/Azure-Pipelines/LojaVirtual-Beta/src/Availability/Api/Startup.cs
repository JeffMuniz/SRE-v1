using Availability.Api.Backend.Application.UseCases.GetLatestAvailability;
using Availability.Api.Backend.Application.UseCases.GetPartnerAvailability;
using Availability.Api.Backend.Domain.Services;
using Availability.Api.Backend.Infrastructure.ExternalServices;
using Availability.Api.Backend.Infrastructure.Web.Filters;
using Availability.Api.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using System;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;

namespace Availability.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMessaging(
                    Configuration.GetConnectionString("ServiceBus"),
                    masstransit =>
                    {
                        masstransit.AddRequestClient<AvailabilityMessaging.Manager.GetLatestAvailability>(TimeSpan.FromMinutes(1));
                        masstransit.AddRequestClient<AvailabilityMessaging.Search.GetAvailability>(TimeSpan.FromMinutes(1));
                    },
                    (provider, config) =>
                    {
                        EndpointConfigurator.MapCommand<AvailabilityMessaging.Manager.GetLatestAvailability>();
                        EndpointConfigurator.MapCommand<AvailabilityMessaging.Search.GetAvailability>();
                    }
                );

            services
                .AddHealthChecks();

            services
                .AddSwaggerGenNewtonsoftSupport()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog Availability Integration Api", Version = "v1" });
                });


            services
                .AddCustomMappers()
                .AddControllers(options =>
                    options.Filters.Add(typeof(HttpResponseExceptionFilter))
                )
                .AddNewtonsoftJson(cfg => cfg.UseMemberCasing());

            services
                .AddScoped<IGetLatestAvailabilityUseCase, GetLatestAvailabilityUseCase>()
                .AddScoped<ILatestAvailabilityService, LatestAvailabilityService>()
                .AddScoped<IPartnerAvailabilityService, PartnerAvailabilityService>()
                .AddScoped<IGetPartnerAvailabilityUseCase, GetPartnerAvailabilityUseCase>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DisplayRequestDuration();
            });

            app
                //.UseHttpsRedirection()
                .UseRouting()
                .UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
