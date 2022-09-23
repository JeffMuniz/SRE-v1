using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Supplier.Shared.Worker.Backend.Domain.Services
{
    public interface ISkuIntegrationService
    {
        Task<Result<bool, ValueObjects.ErrorType>> SkuMustBeIntegrated(ValueObjects.SkuMustBeIntegrated skuMustBeIntegrated, CancellationToken cancellationToken);

        Task IntegrateSku(Entities.SupplierSku supplierSku, CancellationToken cancellationToken);
    }
}
