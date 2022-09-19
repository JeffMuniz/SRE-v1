using Availability.Search.Worker.Backend.Domain.Services;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability.Client;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Cache;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Client;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Shared.Configurations;
using Availability.Search.Worker.Consumers.GetAvailability;
using Availability.Search.Worker.Consumers.GetAvailabilityForAllContracts;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using Shared.Messaging.Contracts.Availability.Messages.Search;
using System;
using BaseWorker = Shared.Worker;
using Usecases = Availability.Search.Worker.Backend.Application.UseCases;

namespace Availability.Search.Worker
{
    public class Startup : BaseWorker.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMemoryCache();

            services
                 .AddMessaging(
                     _configuration.GetConnectionString("ServiceBus"),
                     masstransit =>
                     {
                         masstransit.AddConsumer<GetAvailabilityConsumer>();
                         masstransit.AddConsumer<GetAvailabilityForAllContractsConsumer>();
                     },
                     (provider, config) =>
                     {
                         config.UseMessageRetry(retry =>
                            retry.Interval(2, TimeSpan.FromSeconds(5))
                         );

                         EndpointConfigurator.MapCommand<GetAvailability>();
                         EndpointConfigurator.MapCommand<GetAvailabilityForAllContracts>();
                         EndpointConfigurator.MapEvent<AvailabilityFound>();

                         config.ConfigureReceive<GetAvailability, GetAvailabilityConsumer>(provider);
                         config.ConfigureReceive<GetAvailabilityForAllContracts, GetAvailabilityForAllContractsConsumer>(provider);
                     }
                 );

            services
                .Configure<PartnerHubConfigurationOptions>(_configuration.GetSection("PartnerHub:Configuration"))
                .Configure<PartnerHubAvailabilityOptions>(_configuration.GetSection("PartnerHub:Availability"))
                .Configure<MainContractsConfigurationOptions>(_configuration.GetSection("MainContracts"));

            services
                 .AddHttpClient<IPartnerHubConfigurationClient, PartnerHubConfigurationClient>((provider, httpClient) =>
                 {
                     var options = provider.GetRequiredService<IOptionsMonitor<PartnerHubConfigurationOptions>>();
                     httpClient.BaseAddress = options.CurrentValue.BaseAddress;
                     httpClient.Timeout = options.CurrentValue.Timeout;
                 });

            services
                .AddHttpClient<IPartnerHubClient, PartnerHubClient>((provider, httpClient) =>
                {
                    var options = provider.GetRequiredService<IOptionsMonitor<PartnerHubAvailabilityOptions>>();
                    httpClient.BaseAddress = options.CurrentValue.BaseAddress;
                    httpClient.Timeout = options.CurrentValue.Timeout;
                });

            services
                .AddScoped<ICacheConfiguration, CacheConfiguration>()
                .AddScoped<IAvailabilityService, AvailabilityService>()
                .AddScoped<IConfigurationService, ConfigurationService>()
                .AddScoped<Usecases.GetAvailability.IGetAvailabilityUseCase, Usecases.GetAvailability.GetAvailabilityUseCase>()
                .AddScoped<Usecases.GetAvailabilityForAllContracts.IGetAvailabilityForAllContractsUseCase, Usecases.GetAvailabilityForAllContracts.GetAvailabilityForAllContractsUseCase>();
        }
    }
}
