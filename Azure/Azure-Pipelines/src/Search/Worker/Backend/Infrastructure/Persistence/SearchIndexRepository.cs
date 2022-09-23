using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Search.Worker.Backend.Domain.Services;
using SolrNet;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Search.Worker.Backend.Infrastructure.Persistence
{
    public class SearchIndexRepository : ISearchIndexRepository
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISolrOperations<Models.SearchIndexModel> _solr;

        public SearchIndexRepository(
            ILogger<SearchIndexRepository> logger,
            IMapper mapper,
            ISolrOperations<Models.SearchIndexModel> solr
        )
        {
            _logger = logger;
            _mapper = mapper;
            _solr = solr;
        }

        public Task<Result<Domain.Entities.Sku, Domain.ValueObjects.ErrorType>> Get(
            Domain.ValueObjects.SkuId skuId,
            CancellationToken cancellationToken
        ) => Get(
            new SolrQuery($"{nameof(Models.SearchIndexModel.ProductSkuId)}:{skuId.Value}"),
            cancellationToken
        );

        public Task<Result<Domain.Entities.Sku, Domain.ValueObjects.ErrorType>> Get(
            Shared.Backend.Domain.ValueObjects.SupplierSkuId supplierSkuId,
            CancellationToken cancellationToken
        ) => Get(
            new SolrQuery($"{nameof(Models.SearchIndexModel.PartnerId)}:{supplierSkuId.SupplierId}") &&
            new SolrQuery($"{nameof(Models.SearchIndexModel.OriginalProductSkuId)}:{supplierSkuId.SkuId}"),
            cancellationToken
        );

        public Task<UnitResult<Domain.ValueObjects.ErrorType>> Add(Domain.Entities.Sku sku, CancellationToken cancellationToken) =>
            Upsert(nameof(Add), sku, cancellationToken);

        public Task<UnitResult<Domain.ValueObjects.ErrorType>> Update(Domain.Entities.Sku sku, CancellationToken cancellationToken) =>
            Upsert(nameof(Update), sku, cancellationToken);

        public async Task<UnitResult<Domain.ValueObjects.ErrorType>> Delete(
           Domain.Entities.Sku sku,
            CancellationToken cancellationToken
        )
        {
            var response = await _solr.DeleteAsync(sku.Id.Value);

            return await ConfirmOperation(nameof(Delete), response, cancellationToken);
        }

        private async Task<Result<Domain.Entities.Sku, Domain.ValueObjects.ErrorType>> Get(
            ISolrQuery solrQuery,
            CancellationToken cancellationToken
        )
        {
            var documents = await _solr.QueryAsync(solrQuery, cancellationToken);

            if (documents.NumFound == 0)
                return Domain.ValueObjects.ErrorType.NotFound;

            return _mapper.Map<Domain.Entities.Sku>(documents.First());
        }

        private async Task<UnitResult<Domain.ValueObjects.ErrorType>> Upsert(
            string operation,
            Domain.Entities.Sku sku,
            CancellationToken cancellationToken
        )
        {
            var document = _mapper.Map<Models.SearchIndexModel>(sku);

            return await UpsertDocument(operation, document, cancellationToken);
        }

        private async Task<UnitResult<Domain.ValueObjects.ErrorType>> UpsertDocument(
            string operation,
            Models.SearchIndexModel document,
            CancellationToken cancellationToken
        )
        {
            var response = await _solr.AddAsync(document);

            return await ConfirmOperation(operation, response, cancellationToken);
        }

        private async Task<UnitResult<Domain.ValueObjects.ErrorType>> ConfirmOperation(
            string operation,
            ResponseHeader solrResponseHeader,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (solrResponseHeader.Status != 0)
            {
                _logger.LogError($"{operation} not succeeded");

                await _solr.RollbackAsync();

                return Domain.ValueObjects.ErrorType.FailureOnPersist;
            }

            _logger.LogDebug($"{operation} successfully executed");

            _logger.LogTrace($"Response took {solrResponseHeader.QTime}ms");

            await _solr.CommitAsync();

            return Result.Success<Domain.ValueObjects.ErrorType>();
        }
    }
}
