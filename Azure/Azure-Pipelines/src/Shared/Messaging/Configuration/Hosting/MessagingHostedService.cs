using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Messaging.Configuration.Hosting
{
    public class MessagingHostedService : IHostedService, IDisposable
    {
        private readonly IBusControl _bus;
        private readonly ILogger _logger;

        private bool _stopped;

        public MessagingHostedService(
            ILogger<MessagingHostedService> logger,
            IBusControl bus
        )
        {
            _logger = logger;
            _bus = bus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting {nameof(MessagingHostedService)}");

            return _bus.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_stopped)
                return;

            _logger.LogInformation($"Stopping {nameof(MessagingHostedService)}");

            await _bus.StopAsync(cancellationToken).ConfigureAwait(false);

            _stopped = true;
        }

        public void Dispose()
        {
            if (_stopped)
                return;

            _bus.Stop(TimeSpan.FromSeconds(30));

            _stopped = true;
        }
    }
}
