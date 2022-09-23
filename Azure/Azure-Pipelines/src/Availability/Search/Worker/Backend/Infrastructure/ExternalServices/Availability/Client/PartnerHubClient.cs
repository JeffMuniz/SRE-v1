using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Shared.Configurations;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability.Client
{
    public class PartnerHubClient : Shared.Client.BaseClient, IPartnerHubClient
    {
        private readonly IOptionsMonitor<PartnerHubAvailabilityOptions> _options;

        public PartnerHubClient(
            ILogger<PartnerHubClient> logger,
            HttpClient httpClient,
            IOptionsMonitor<PartnerHubAvailabilityOptions> options
        )
            : base(logger, httpClient)
        {
            _options = options;
        }

        public async Task<Result<Models.AvailabilityResult, HttpStatusCode>> GetAvailability(
            Models.AvailabilityRequest availabilityRequest,
            CancellationToken cancellationToken = default
        )
        {
            var uri = _options.CurrentValue.AvailabilityEndpoint.OriginalString
                .Replace(":partner_code", availabilityRequest.PartnerCode)
                .Replace(":sku_id", availabilityRequest.SupplierSkuId)
                .Replace(":contract", availabilityRequest.ContractId);

            _logger.LogDebug("Sending request to get availability in url {uri}", uri);

            var response = await ExecuteWithRetry(cancel =>
                _httpClient.GetAsync(uri, cancel),
                cancellationToken
            );

            if (response.StatusCode.Is(HttpStatusCode.UnprocessableEntity))
            {
                var error = await DeserializeContent<Models.Error>(response.Content, cancellationToken);
                if (error.Errors.Any(e => e.Contains("inexistente")))
                    return HttpStatusCode.NotFound;
            }

            response.EnsureSuccessStatusCode();

            return await DeserializeContent<Models.AvailabilityResult>(response.Content, cancellationToken);
        }

        protected override Task AssignCredentials(HttpClient httpClient, CancellationToken cancellationToken)
        {
            if (!httpClient.DefaultRequestHeaders.Contains(_options.CurrentValue.Authentication.SubscriptionKey.Name))
                httpClient.DefaultRequestHeaders.Add(_options.CurrentValue.Authentication.SubscriptionKey.Name, _options.CurrentValue.Authentication.SubscriptionKey.Value);

            return Task.CompletedTask;
        }
    }
}
