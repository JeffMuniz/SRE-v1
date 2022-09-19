using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Job.Abstractions.Availability.Recovery;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Recovery.Worker.Jobs
{
    public class AvailabilityRecoveryHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IOptions<Shared.Job.Configuration.Options.JobOptions> _jobOptions;
        private readonly IOptionsMonitor<List<JobScheduleConfiguration>> _options;

        private IDisposable _listener;

        public AvailabilityRecoveryHostedService(
            ILogger<AvailabilityRecoveryHostedService> logger,
            IOptions<Shared.Job.Configuration.Options.JobOptions> jobOptions,
            IOptionsMonitor<List<JobScheduleConfiguration>> options
        )
        {
            _logger = logger;
            _options = options;
            _jobOptions = jobOptions;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _listener = _options.OnChange((config, value) =>
            {
                StopAsync(cancellationToken)
                    .ContinueWith(_ => StartAsync(cancellationToken));
            });

            var lastOption = default(JobScheduleConfiguration);
            foreach (var option in _options.CurrentValue)
            {
                var initialTime = option.TimeLapse;
                var endTime = lastOption?.TimeLapse;

                RecurringJob.AddOrUpdate<IAvailabilityRecoveryRecurringJob>(
                    AvailabilityRecoveryRecurringJob.GetFormmatedJobName(option.TimeLapse),
                    job => job.Execute(
                        new AvailabilityRecoveryJobContext
                        {
                            PageSize = option.PageSize,
                            InitialTime = initialTime,
                            EndTime = endTime
                        },
                        cancellationToken
                    ),
                    option.CronExpression,
                    TimeZoneInfoExtensions.GetBrazilianTimeZone(),
                    _jobOptions.Value.QueueName
                );

                lastOption = option;
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _ = cancellationToken;
            _logger.LogInformation($"Stopping {AvailabilityRecoveryRecurringJob.JOB_NAME}*");

            //foreach (var option in _options.CurrentValue)
            //    RecurringJob.RemoveIfExists(GetFormmatedJobName(option.TimeLapse));

            return Task.CompletedTask;
        }

        #region [ Dispose ]

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _listener?.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AvailabilityRecoveryHostedService()
        {
            Dispose(false);
        }

        #endregion [ Dispose ]
    }
}
