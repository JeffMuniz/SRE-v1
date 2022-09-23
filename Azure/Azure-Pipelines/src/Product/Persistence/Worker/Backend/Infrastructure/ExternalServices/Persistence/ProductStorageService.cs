using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Product.Persistence.Worker.Backend.Domain.Services;
using Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Configurations;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence
{
    public class ProductStorageService : IProductStorageService, IDisposable
    {
        private const string KEY_AUTHENTICATION_IS_VALID = "AUTHENTICATION_IS_VALID";

        private readonly IMapper _mapper;
        private readonly SemaphoreSlim _authenticationSemaphore = new(1, 1);
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<StorageClientConfiguration> _options;
        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;

        private AuthenticationHeaderValue AuthenticationToken { get; set; }

        protected static JsonSerializerOptions SerializeOptions => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        protected static JsonSerializerOptions DeserializeOptions => new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductStorageService(
            IMapper mapper,
            ILogger<ProductStorageService> logger,
            IOptionsMonitor<StorageClientConfiguration> options,
            IMemoryCache cache,
            HttpClient httpClient
        )
        {
            _mapper = mapper;
            _logger = logger;
            _options = options;
            _cache = cache;
            _httpClient = httpClient;
        }

        public async Task<Domain.ValueObjects.PersistedData> Store(Domain.Entities.Product product, CancellationToken cancellationToken = default)
        {
            var uri = _options.CurrentValue.StoreSkuEndpoint.OriginalString
                .Replace(":supplierId", product.SupplierId.ToString())
                .Replace(":originalProductSkuId", product.Sku.Id.ToString());

            _logger.LogDebug("Sending request to persist product in url {uri}", uri);

            var model = _mapper.Map<Models.Request.Product>(product);
            var content = SerializeStringContent(model);

            //TODO: Ajustar a forma como vai pegar o CorrelationId do contexto para enviar na request
            _httpClient.DefaultRequestHeaders.Add("IdSolicitacao", Guid.NewGuid().ToString());

            var response = await ExecuteAuthenticatedRequest(cancel =>
                _httpClient.PostAsync(uri, content, cancel)
            , cancellationToken);

            response.EnsureSuccessStatusCode();

            var storedResult = await DeserializeContent<Models.Response.StoredResult>(response.Content, cancellationToken);
            return _mapper.Map<Domain.ValueObjects.PersistedData>(storedResult);
        }

        public async Task<UnitResult<Domain.ValueObjects.ErrorType>> StoreAvailability(Domain.ValueObjects.SkuAvailability skuAvailability, CancellationToken cancellationToken = default)
        {
            var uri = _options.CurrentValue.StoreAvailabilityEndpoint.OriginalString
                .Replace(":supplierId", skuAvailability.SupplierId.ToString())
                .Replace(":originalProductSkuId", skuAvailability.SupplierSkuId.ToString());

            _logger.LogDebug("Sending request to inactivate product in url {uri}", uri);

            var model = _mapper.Map<Models.Request.SkuAvailability>(skuAvailability);
            var content = SerializeStringContent(model);

            //TODO: Ajustar a forma como vai pegar o CorrelationId do contexto para enviar na request
            _httpClient.DefaultRequestHeaders.Add("IdSolicitacao", Guid.NewGuid().ToString());

            var response = await ExecuteAuthenticatedRequest(cancel =>
                _httpClient.PutAsync(uri, content, cancel)
            , cancellationToken);

            if (response.StatusCode.Is(HttpStatusCode.UnprocessableEntity))
            {
                var errorResult = await DeserializeContent<Models.Response.Error>(response.Content, cancellationToken);
                if (errorResult.Message.Contains("[NotFound]"))
                    return UnitResult.Failure(Domain.ValueObjects.ErrorType.NotFound);
            }

            response.EnsureSuccessStatusCode();

            return UnitResult.Success<Domain.ValueObjects.ErrorType>();
        }

        private async Task<HttpResponseMessage> ExecuteAuthenticatedRequest(Func<CancellationToken, Task<HttpResponseMessage>> action, CancellationToken cancellationToken)
        {
            var policyResult = await Policy
                   .Handle<HttpRequestException>(error =>
                       error.StatusCode == HttpStatusCode.Unauthorized
                   )
                   .WaitAndRetryAsync(
                       1,
                       retryCount => TimeSpan.FromSeconds(retryCount),
                       (error, retryTime, retryCount, _) => AuthenticationToken = default
                   )
                   .ExecuteAndCaptureAsync(async cancel =>
                   {
                       _httpClient.DefaultRequestHeaders.Authorization = await GetAuthentication(cancel);

                       var response = await action(cancel);

                       if (response.StatusCode.Is(HttpStatusCode.Unauthorized))
                           response.EnsureSuccessStatusCode();

                       return response;
                   }, cancellationToken);

            if (policyResult.Outcome == OutcomeType.Failure)
                throw policyResult.FinalException;

            return policyResult.Result;
        }

        protected async Task<AuthenticationHeaderValue> GetAuthentication(CancellationToken cancellationToken)
        {
            try
            {
                await _authenticationSemaphore.WaitAsync(cancellationToken);

                if (AuthenticationToken != default && AuthenticationIsValid())
                    return AuthenticationToken;

                var uri = _options.CurrentValue.Authentication.TokenEndpoint;

                _logger.LogDebug("Fazendo request token de autenticação {uri}", uri);

                var username = _options.CurrentValue.Authentication.Username;
                var password = _options.CurrentValue.Authentication.Password;

                var credencials = new
                {
                    username,
                    password
                };

                var content = SerializeStringContent(credencials);
                var response = await _httpClient.PostAsync(uri, content, cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseContent = await DeserializeContent<Models.Authentication.AccessToken>(response.Content, cancellationToken);

                _cache.GetOrCreate(KEY_AUTHENTICATION_IS_VALID,
                    entry => entry
                        .SetValue(true)
                        .SetAbsoluteExpiration(responseContent.ExpirationDate)
                        .Value
                );

                return AuthenticationToken = new AuthenticationHeaderValue("bearer", responseContent.Token);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "Ocorreu um erro durante a obtenção do token de autenticação");
                throw;
            }
            finally
            {
                _authenticationSemaphore.Release();
            }
        }

        protected virtual bool AuthenticationIsValid() =>
            _cache.TryGetValue(KEY_AUTHENTICATION_IS_VALID, out var _);

        protected static StringContent SerializeStringContent<T>(T @object)
        {
            var options = SerializeOptions;
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            var json = JsonSerializer.Serialize(@object, options);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected static async Task<T> DeserializeContent<T>(HttpContent content, CancellationToken cancellationToken)
        {
            var options = DeserializeOptions;
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            return await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync(cancellationToken), options, cancellationToken);
        }



        #region [Dispose]

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _authenticationSemaphore.Dispose();
            }

            _disposed = true;
        }

        ~ProductStorageService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion [Dispose]
    }
}
