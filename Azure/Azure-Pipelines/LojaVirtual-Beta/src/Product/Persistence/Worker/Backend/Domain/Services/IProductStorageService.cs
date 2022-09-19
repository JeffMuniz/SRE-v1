using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Persistence.Worker.Backend.Domain.Services
{
    public interface IProductStorageService
    {
        Task<ValueObjects.PersistedData> Store(Entities.Product productLegacy, CancellationToken cancellation);

        Task<UnitResult<ValueObjects.ErrorType>> StoreAvailability(ValueObjects.SkuAvailability inactivateSku, CancellationToken cancellationToken);
    }
}
