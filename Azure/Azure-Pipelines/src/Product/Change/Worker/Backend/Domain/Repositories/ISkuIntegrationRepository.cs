using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Domain.Repositories
{
    public interface ISkuIntegrationRepository
    {
        Task<Maybe<Entities.SkuIntegration>> Get(SharedDomain.ValueObjects.SupplierSkuId id, CancellationToken cancellationToken);

        Task<Maybe<Entities.SkuIntegration>> Get(ValueObjects.SkuIntegrationId id, CancellationToken cancellationToken);

        Task<Maybe<Entities.SkuIntegration>> Add(Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken);

        Task Update(Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken);
    }
}
