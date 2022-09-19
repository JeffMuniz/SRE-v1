using AutoMapper;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu
{
    public class MagaluIntegrationService : Domain.Services.IMagaluIntegrationService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<MagaluConfigurationOptions> _options;

        public MagaluIntegrationService(
            ILogger<MagaluIntegrationService> logger,
            IMapper mapper,
            HttpClient httpClient,
            IOptionsMonitor<MagaluConfigurationOptions> options
        )
        {
            _logger = logger;
            _mapper = mapper;
            _httpClient = httpClient;
            _options = options;
        }

        public async IAsyncEnumerable<Domain.Entities.Sku> GetCatalog([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var url = _options.CurrentValue.ProductEndpoint.Url;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", _options.CurrentValue.ProductEndpoint.Authorization);

            var cursor = Models.Cursor.Init();

            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                var formmatedUrl = url.FormatCursorAndPageSize(cursor.Next, _options.CurrentValue.ProductEndpoint.PageSize);
                _logger.LogDebug($"Getting {nameof(Domain.Entities.Sku)}s from {formmatedUrl}");

                var totalRetryCount = _options.CurrentValue.ProductEndpoint.RetryCount;
                var streamRetry = await Policy
                    .Handle<HttpRequestException>()
                    .WaitAndRetryAsync(
                        totalRetryCount,
                        retryAttempOn =>
                            TimeSpan.FromSeconds(Math.Pow(2, retryAttempOn)),
                        (error, waitTime, retryCount, context) =>
                            _logger.LogWarning(error,
                                $"Erro na obtenção do catálogo. Tentativa: {retryCount} de {totalRetryCount}" +
                                $"Ao tentar obter dados da url: {formmatedUrl}"
                            )
                    )
                    .ExecuteAndCaptureAsync(async (stopping) =>
                    {
                        var responseMessage = await _httpClient.GetAsync(formmatedUrl, stopping);

                        responseMessage.EnsureSuccessStatusCode();

                        return await responseMessage.Content.ReadAsStreamAsync(stopping);
                    }, cancellationToken);

                if (streamRetry.Outcome == OutcomeType.Failure)
                    throw streamRetry.FinalException;

                using (var stream = streamRetry.Result)
                using (var streamReader = new StreamReader(stream, Encoding.UTF8, true))
                using (var reader = XmlReader.Create(streamReader, new XmlReaderSettings { Async = true }))
                {
                    var document = new XmlDocument();
                    await reader.MoveToContentAsync();

                    while (!reader.EOF && await reader.ReadAsync())
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        if (reader.LocalName is not "row")
                            continue;

                        using (var nodeReader = reader.ReadSubtree())
                        {
                            var node = document.ReadNode(nodeReader);
                            var json = JsonConvert.SerializeXmlNode(node, Newtonsoft.Json.Formatting.None, true);

                            if (node.Attributes.GetNamedItem("hasNext") != null)
                            {
                                cursor = JsonConvert.DeserializeObject<Models.Cursor>(json);
                                continue;
                            }

                            var model = JsonConvert.DeserializeObject<Models.Sku>(json);
                            yield return _mapper.Map<Domain.Entities.Sku>(model);
                        }
                    }
                }
            } while (cursor.HasNext);
        }

        public async IAsyncEnumerable<Domain.Entities.Color> GetColors([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var url = _options.CurrentValue.ColorEndpoint;

            _logger.LogDebug($"Getting {nameof(Domain.Entities.Color)}s from {url}");

            _httpClient.DefaultRequestHeaders.Clear();

            using (var stream = await _httpClient.GetStreamAsync(url, cancellationToken))
            using (var streamReader = new StreamReader(stream, Encoding.UTF8, true))
            using (var reader = XmlReader.Create(streamReader, new XmlReaderSettings { Async = true }))
            {
                var document = new XmlDocument();
                var colors = Enumerable.Empty<Domain.Entities.Color>();

                await reader.MoveToContentAsync();

                while (!reader.EOF && await reader.ReadAsync())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (reader.LocalName is not "row")
                        continue;

                    using (var nodeReader = reader.ReadSubtree())
                    {
                        var node = document.ReadNode(nodeReader);
                        var json = JsonConvert.SerializeXmlNode(node, Newtonsoft.Json.Formatting.None, true);
                        var model = JsonConvert.DeserializeObject<Models.Color>(json);
                        yield return _mapper.Map<Domain.Entities.Color>(model);
                    }
                }
            }
        }

        public async IAsyncEnumerable<Domain.ValueObjects.Specification> GetSpecifications(string skuId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var url = _options.CurrentValue.FeatureEndpoint.FormatProductId(skuId);

            _logger.LogDebug($"Getting {nameof(Domain.ValueObjects.Specification)}s from {url}");

            var totalRetryCount = _options.CurrentValue.ProductEndpoint.RetryCount;
            var streamRetry = await Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(
                    totalRetryCount,
                    retryAttempOn =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempOn)),
                    (error, waitTime, retryCount, context) =>
                        _logger.LogWarning(error,
                            $"Erro na obtenção das especificações do sku. Tentativa: {retryCount} de {totalRetryCount}" +
                            $"Ao tentar obter dados da url: {url}"
                        )
                )
                .ExecuteAndCaptureAsync(async (stopping) =>
                {
                    var responseMessage = await _httpClient.GetAsync(url, stopping);

                    responseMessage.EnsureSuccessStatusCode();

                    return await responseMessage.Content.ReadAsStreamAsync(stopping);
                }, cancellationToken);

            if (streamRetry.Outcome == OutcomeType.Failure)
                throw streamRetry.FinalException;

            using (var stream = streamRetry.Result)
            using (var streamReader = new StreamReader(stream, Encoding.UTF8, true))
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.Load(streamReader);

                var nodes = htmlDocument.DocumentNode.SelectNodes(".//li");
                if (nodes == null)
                    yield break;

                foreach (var spec in nodes)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var model = Models.Specification.Create(spec.InnerText?.Trim());
                    yield return _mapper.Map<Domain.ValueObjects.Specification>(model);
                }
            }

        }
    }
}
