using System.Collections.Generic;
using System.Threading;

namespace Product.Supplier.Magalu.Worker.Backend.Domain.Services
{
    public interface IMagaluIntegrationService
    {
        IAsyncEnumerable<Entities.Sku> GetCatalog(CancellationToken cancellationToken);

        IAsyncEnumerable<Entities.Color> GetColors(CancellationToken cancellationToken);

        IAsyncEnumerable<ValueObjects.Specification> GetSpecifications(string skuId, CancellationToken cancellationToken);
    }
}
