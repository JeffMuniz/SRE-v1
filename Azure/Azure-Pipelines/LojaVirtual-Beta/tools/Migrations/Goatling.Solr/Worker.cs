using Goatling.Solr.Enriched;
using Goatling.Solr.Products;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using SolrNet;
using SolrNet.Commands.Parameters;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Goatling.Solr
{
    public class Worker : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _provider;

        public Worker(ILogger<Worker> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var _products = scope.ServiceProvider.GetRequiredService<ISolrOperations<Doc>>();
                    var _enriched = scope.ServiceProvider.GetRequiredService<ISolrOperations<SearchIndexModel>>();

                    var total = _products.Query(SolrQuery.All, new QueryOptions { Rows = 0 }).NumFound;

                    const int totalRetryCount = 10;
                    const int step = 5000;
                    var start = 0;
                    var hit = step;

                    _logger.LogInformation($"Total de {total} documento no core origem.");
                    while (start < total)
                    {
                        var policyRetry = await Policy
                            .Handle<HttpRequestException>()
                            .WaitAndRetryAsync(
                                totalRetryCount,
                                retryAttempOn =>
                                    TimeSpan.FromSeconds(retryAttempOn),
                                (error, waitTime, retryCount, context) =>
                                    _logger.LogWarning(error,
                                        $"Erro na requisição. Tentativa: {retryCount} de {totalRetryCount} " +
                                        $"Registros {start} até {start + hit}"
                                    )
                            )
                            .ExecuteAndCaptureAsync(async (stopping) =>
                            {
                                var list = _products.Query(
                                    SolrQuery.All,
                                    new QueryOptions
                                    {
                                        StartOrCursor = new StartOrCursor.Start(start),
                                        Rows = hit
                                    })
                                    .AsReadOnly()
                                    .Select(doc => SearchIndexModel.Create(
                                        doc.Group,
                                        doc.ProductSkuId,
                                        doc.OriginalProductSkuId,
                                        doc.Name,
                                        doc.Description,
                                        doc.EAN,
                                        doc.CategoryId,
                                        doc.Category,
                                        doc.SubCategoryId,
                                        doc.Subcategory,
                                        doc.BrandId,
                                        doc.Brand,
                                        doc.PartnerId,
                                        doc.Partner,
                                        doc.PriceFor,
                                        doc.Image,
                                        doc.Relevance,
                                        doc.KeyWord,
                                        doc.Features,
                                        (doc.CreatedDate?.Count == null || doc.CreatedDate.Count == 0)
                                            ? DateTime.Now
                                            : doc.CreatedDate.FirstOrDefault(),
                                        doc.BestSellerOrdination
                                    ));

                                await _enriched.AddRangeAsync(list);
                            }, stoppingToken);

                        if (policyRetry.Outcome == OutcomeType.Failure)
                            throw policyRetry.FinalException;

                        _logger.LogInformation($"registro {start} até {start + hit} concluído");

                        start += hit;
                        hit = (start + step > total)
                            ? total - start
                            : step;
                    }

                    await _enriched.CommitAsync();

                    _logger.LogInformation("Migração concluída!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro durante a execução.");
            }
        }
    }
}
