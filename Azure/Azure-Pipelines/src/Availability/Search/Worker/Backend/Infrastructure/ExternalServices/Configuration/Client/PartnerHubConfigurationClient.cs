using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Shared.Configurations;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Client
{
    public class PartnerHubConfigurationClient : Shared.Client.BaseClient, IPartnerHubConfigurationClient
    {
        private readonly IOptionsMonitor<PartnerHubConfigurationOptions> _options;

        public PartnerHubConfigurationClient(
            ILogger<PartnerHubConfigurationClient> logger,
            HttpClient httpClient,
            IOptionsMonitor<PartnerHubConfigurationOptions> options
        )
            : base(logger, httpClient)
        {
            _options = options;
        }

        public async Task<Result<Models.AllContractsResult>> GetAllContracts(
            CancellationToken cancellationToken = default
        )
        {
            var uri = _options.CurrentValue.ContractsEndpoint.OriginalString;

            _logger.LogDebug("Sending request to get configuration in url {uri}", uri);

            var response = await ExecuteWithRetry(cancel =>
                _httpClient.GetAsync(uri, cancel),
                cancellationToken
            );

            response.EnsureSuccessStatusCode();

            return await DeserializeContent<Models.AllContractsResult>(response.Content, cancellationToken);
        }

        public async Task<Result<Models.AllPartnersResult>> GetAllPartners(
            CancellationToken cancellationToken = default
        )
        {
            var uri = _options.CurrentValue.PartnersEndpoint.OriginalString;

            _logger.LogDebug("Sending request to get configuration in url {uri}", uri);

            var response = await ExecuteWithRetry(cancel =>
                _httpClient.GetAsync(uri, cancel),
                cancellationToken
            );

            response.EnsureSuccessStatusCode();

            return await DeserializeContent<Models.AllPartnersResult>(response.Content, cancellationToken);
        }

        public async Task<Result<Models.ContractParametersResult, HttpStatusCode>> GetContractParameters(
            string contract,
            string connector,
            CancellationToken cancellationToken = default
        )
        {
            var uri = _options.CurrentValue.ContractParametersEndpoint.OriginalString
                .Replace("{contract}", contract)
                .Replace("{connector}", connector);

            _logger.LogDebug("Sending request to get configuration in url {uri}", uri);

            var response = await ExecuteWithRetry(cancel =>
                _httpClient.GetAsync(uri, cancel),
                cancellationToken
            );

            if (response.StatusCode.Is(HttpStatusCode.NotFound))
                return response.StatusCode;

            response.EnsureSuccessStatusCode();

            return await DeserializeContent<Models.ContractParametersResult>(response.Content, cancellationToken);
        }

        protected override Task AssignCredentials(HttpClient httpClient, CancellationToken cancellationToken)
        {
            if (!httpClient.DefaultRequestHeaders.Contains(_options.CurrentValue.Authentication.SubscriptionKey.Name))
                httpClient.DefaultRequestHeaders.Add(_options.CurrentValue.Authentication.SubscriptionKey.Name, _options.CurrentValue.Authentication.SubscriptionKey.Value);

            if (httpClient.DefaultRequestHeaders.Authorization is null)
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _options.CurrentValue.Authentication.Basic);

            return Task.CompletedTask;
        }
    }
}
