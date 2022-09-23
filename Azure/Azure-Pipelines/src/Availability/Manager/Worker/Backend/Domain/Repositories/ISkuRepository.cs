using Availability.Manager.Worker.Backend.Domain.Entities;
using CSharpFunctionalExtensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using SharedValueObjects = Shared.Backend.Domain.ValueObjects;

namespace Availability.Manager.Worker.Backend.Domain.Repositories
{
    public interface ISkuRepository
    {
        Task<Maybe<SkuAvailability>> Get(ValueObjects.SkuAvailabilityId id, CancellationToken cancellationToken);

        Task<Maybe<SkuAvailability>> Get(SharedValueObjects.SupplierSkuId supplierSkuId, CancellationToken cancellationToken);

        Task<ValueObjects.PagedSkuAvailability> GetUnavailable(ValueObjects.SkuUnavailableSearchFilter searchFilter, CancellationToken cancellationToken);

        Task<SkuAvailability> Add(SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task<bool> Update(SkuAvailability skuAvailability, CancellationToken cancellationToken);

        Task<bool> UpdateOnlyLatestPartnerAvailabilityFoundDate(
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        );

        Task<bool> RemoveAllContracts(SkuAvailability skuAvailability, CancellationToken cancellationToken);
    }
}
