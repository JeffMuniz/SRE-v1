using Integration.Api.Backend.Application.Offer.UseCases.NotifyPendings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.BackgroundServices
{
    public class OfferNotificationBackgroundService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptionsMonitor<OfferNotificationBackgroundServiceOptions> _options;

        public OfferNotificationBackgroundService(
            ILogger<OfferNotificationBackgroundService> logger,
            IServiceProvider serviceProvider,
            IOptionsMonitor<OfferNotificationBackgroundServiceOptions> options
        )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_options.CurrentValue.Enabled)
                return;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"Executing offers notification at {DateTime.Now}");

                    using var scope = _serviceProvider.CreateScope();
                    var notifyPendingsUseCase = scope.ServiceProvider.GetRequiredService<INotifyPendingsUseCase>();

                    await notifyPendingsUseCase.Execute(_options.CurrentValue.DegreeOfParallelism, stoppingToken);

                    _logger.LogInformation($"Offers notification completed at {DateTime.Now}");

                    await Task.Delay(_options.CurrentValue.IntervalTime, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    // No action required
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred executing offers notification at {DateTime.Now}");
                }
            }
        }
    }
}
