using CSharpFunctionalExtensions;
using Integration.Api.Backend.Domain.ExternalServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Infrastructure.ExternalServices
{
    public class MacnaimaService : IMacnaimaService
    {
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<MacnaimaServiceSettings> _settings;
        private readonly HttpClient _httpClient;

        public MacnaimaService(
            ILogger<MacnaimaService> logger,
            IOptionsMonitor<MacnaimaServiceSettings> settings,
            HttpClient httpClient
        )
        {
            _logger = logger;
            _settings = settings;
            _httpClient = httpClient;
        }

        public Task<Result> NotifyOffer(string offerId, CancellationToken cancellationToken) =>
            NotifyOffer(_settings.CurrentValue.DefaultStore, offerId, cancellationToken);

        public async Task<Result> NotifyOffer(string store, string offerId, CancellationToken cancellationToken)
        {
            var endpoint = $"/store/{store}/notification";

            var content = System.Net.Http.Json.JsonContent.Create(new
            {
                id = offerId
            });

            var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);

            var responseString = $"Status: {response.StatusCode} | Content: {await response.Content.ReadAsStringAsync(cancellationToken)}";
            var metadata = JsonConvert.SerializeObject(new { Endpoint = endpoint, response.StatusCode, Store = store, OfferId = offerId });

            _logger.LogInformation($"Metadata: {metadata} - {responseString}");

            if (!response.IsSuccessStatusCode)
                return Result.Failure(responseString);

            return Result.Success();
        }
    }
}
