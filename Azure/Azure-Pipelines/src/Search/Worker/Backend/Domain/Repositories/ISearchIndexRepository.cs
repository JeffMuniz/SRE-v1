using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedDomain = Shared.Backend.Domain;

namespace Search.Worker.Backend.Domain.Services
{
    public interface ISearchIndexRepository
    {
        Task<Result<Entities.Sku, ValueObjects.ErrorType>> Get(ValueObjects.SkuId skuId, CancellationToken cancellationToken);

        Task<Result<Entities.Sku, ValueObjects.ErrorType>> Get(SharedDomain.ValueObjects.SupplierSkuId supplierSkuId, CancellationToken cancellationToken);

        Task<UnitResult<ValueObjects.ErrorType>> Add(Entities.Sku sku, CancellationToken cancellationToken);

        Task<UnitResult<ValueObjects.ErrorType>> Update(Entities.Sku sku, CancellationToken cancellationToken);

        Task<UnitResult<ValueObjects.ErrorType>> Delete(Domain.Entities.Sku sku, CancellationToken cancellationToken);
    }
}
