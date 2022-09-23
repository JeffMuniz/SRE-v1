using Humanizer;
using Microsoft.Extensions.Logging;
using Shared.Job.Abstractions.Availability.Recovery;
using System;
using System.Threading;
using System.Threading.Tasks;
using Usecases = Availability.Recovery.Worker.Backend.Application.Usecases;

namespace Availability.Recovery.Worker.Jobs
{
    internal class AvailabilityRecoveryRecurringJob : IAvailabilityRecoveryRecurringJob
    {
        public const string JOB_NAME = "Availability.Recovery";

        private readonly ILogger<AvailabilityRecoveryRecurringJob> _logger;
        private readonly Usecases.AvailabilityRecovery.IAvailabilityRecoveryUseCase _availabilityRecoveryUseCase;

        public string JobName => JOB_NAME;

        public AvailabilityRecoveryRecurringJob(
            ILogger<AvailabilityRecoveryRecurringJob> logger,
            Usecases.AvailabilityRecovery.IAvailabilityRecoveryUseCase availabilityRecoveryUseCase
        )
        {
            _logger = logger;
            _availabilityRecoveryUseCase = availabilityRecoveryUseCase;
        }

        public async Task Execute(AvailabilityRecoveryJobContext context, CancellationToken cancellationToken)
        {
            var inbound = new Usecases.AvailabilityRecovery.Models.Inbound
            {
                PageSize = context.PageSize,
                InitialDateTime = DateTime.UtcNow.Subtract(context.InitialTime),
                EndDateTime = context.EndTime.HasValue
                    ? DateTime.UtcNow.Subtract(context.EndTime.Value)
                    : DateTime.UtcNow
            };

            _logger.LogInformation("Starting availability recovery at {InitialDateTime} and {EndDateTime}", inbound.InitialDateTime, inbound.EndDateTime);

            var outbound = await _availabilityRecoveryUseCase.Execute(inbound, cancellationToken);
            if (outbound.IsFailure)
            {
                _logger.LogWarning("Failure availability recovery at {InitialDateTime} and {EndDateTime}. Error: {Error}", inbound.InitialDateTime, inbound.EndDateTime, outbound.Error);

                return;
            }

            _logger.LogInformation("Success on availability recovery at  {InitialDateTime} and {EndDateTime}.", inbound.InitialDateTime, inbound.EndDateTime);
        }

        public static string GetFormmatedJobName(TimeSpan timeLapse)
        {
            var timeLapseName = timeLapse.Humanize(collectionSeparator: "-").Dehumanize();
            return $"{JOB_NAME}.{timeLapseName}";
        }
    }
}
