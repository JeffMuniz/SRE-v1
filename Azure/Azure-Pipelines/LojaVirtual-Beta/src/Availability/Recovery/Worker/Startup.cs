using Availability.Recovery.Worker.Backend.Application.Usecases.AvailabilityRecovery;
using Availability.Recovery.Worker.Backend.Domain.Services;
using Availability.Recovery.Worker.Backend.Infrastructure.ExternalServices.Availability;
using Availability.Recovery.Worker.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Job.Abstractions.Availability.Recovery;
using Shared.Job.Configuration.Extensions;
using Shared.Messaging.Configuration;
using Shared.Messaging.Configuration.Extensions;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using System;
using System.Collections.Generic;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;
using BaseWorker = Shared.Worker;

namespace Availability.Recovery.Worker
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
                .AddJobs(_configuration)
                .AddJobsServer(_configuration);

            services
                .AddMessaging(
                    _configuration.GetConnectionString("ServiceBus"),
                    masstransit =>
                    {
                        masstransit.AddRequestClient<GetUnavailableSkus>(TimeSpan.FromMinutes(1));
                    },
                    (provider, config) =>
                    {
                        EndpointConfigurator.MapCommand<GetUnavailableSkus>();
                        EndpointConfigurator.MapCommand<AvailabilityMessages.Search.GetAvailability>();                        
                    }
                );            

            services
                .AddHostedService<AvailabilityRecoveryHostedService>()
                .Configure<List<JobScheduleConfiguration>>(_configuration.GetSection("JobSchedules"))
                .AddScoped<IAvailabilityRecoveryRecurringJob, AvailabilityRecoveryRecurringJob>();

            services
                .AddScoped<IAvailabilityRecoveryService, AvailabilityRecoveryService>()
                .AddScoped<IAvailabilityRecoveryUseCase, AvailabilityRecoveryUseCase>();
        }
    }
}
