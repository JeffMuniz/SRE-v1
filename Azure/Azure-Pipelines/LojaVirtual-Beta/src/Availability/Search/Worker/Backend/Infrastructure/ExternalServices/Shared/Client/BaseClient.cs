using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Shared.Configurations;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Shared.Client
{
    public abstract class BaseClient
    {
        protected readonly JsonSerializerOptions _deserializeOptions = CreateDeserializerOptions();
        protected readonly ILogger _logger;
        protected readonly HttpClient _httpClient;

        protected BaseClient(
            ILogger logger,
            HttpClient httpClient
        )
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        protected async Task<HttpResponseMessage> ExecuteWithRetry(
            Func<CancellationToken, Task<HttpResponseMessage>> action,
            CancellationToken cancellationToken
        )
        {
            var policyResult = await Policy
                   .Handle<HttpRequestException>(error =>
                       error.StatusCode.GetValueOrDefault().In(HttpStatusCode.GatewayTimeout, HttpStatusCode.RequestTimeout)
                   )
                   .WaitAndRetryAsync(
                       1,
                       retryCount => TimeSpan.FromSeconds(retryCount)
                   )
                   .ExecuteAndCaptureAsync(async cancel =>
                   {
                       await AssignCredentials(_httpClient, cancel);

                       return await action(cancel);
                   }, cancellationToken);

            if (policyResult.Outcome == OutcomeType.Failure)
                throw policyResult.FinalException;

            return policyResult.Result;
        }

        protected virtual Task AssignCredentials(HttpClient httpClient, CancellationToken cancellationToken) =>
            Task.CompletedTask;

        protected async Task<T> DeserializeContent<T>(HttpContent content, CancellationToken cancellationToken) =>
            await JsonSerializer.DeserializeAsync<T>(
                await content.ReadAsStreamAsync(cancellationToken),
                _deserializeOptions,
                cancellationToken
            );

        private static JsonSerializerOptions CreateDeserializerOptions()
        {
            var deserializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            deserializeOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            return deserializeOptions;
        }
    }
}
