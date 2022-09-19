using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Job.Abstractions.Product.Supplier.Magalu;
using Shared.Job.Configuration.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Supplier.Magalu.Worker.Jobs
{
    public class MagaluIntegrateCatalogHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IOptions<JobOptions> _jobOptions;
        private readonly IOptionsMonitor<JobScheduleConfiguration> _options;
        private IDisposable _listener;

        public MagaluIntegrateCatalogHostedService(
            ILogger<MagaluIntegrateCatalogHostedService> logger,
            IOptions<JobOptions> jobOptions,
            IOptionsMonitor<JobScheduleConfiguration> options
        )
        {
            _logger = logger;
            _jobOptions = jobOptions;
            _options = options;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _listener?.Dispose();
            _listener = _options.OnChange((config, value) =>
            {
                StopAsync(cancellationToken)
                    .ContinueWith(_ => StartAsync(cancellationToken));
            });

            RecurringJob.AddOrUpdate<IMagaluIntegrateCatalogRecurringJob>(
                MagaluIntegrateCatalogRecurringJob.JOB_NAME,
                job => job.Execute(this, cancellationToken),
                _options.CurrentValue.CronExpression,
                TimeZoneInfoExtensions.GetBrazilianTimeZone(),
                _jobOptions.Value.QueueName
            );

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _ = cancellationToken;
            _logger.LogInformation($"Stopping {MagaluIntegrateCatalogRecurringJob.JOB_NAME}");

            //RecurringJob.RemoveIfExists(MagaluIntegrateCatalogRecurringJob.JOB_NAME);

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

        ~MagaluIntegrateCatalogHostedService()
        {
            Dispose(false);
        }

        #endregion [ Dispose ]
    }
}
