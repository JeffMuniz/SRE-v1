using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Recovery.Worker.Backend.Domain.Services
{
    public interface IAvailabilityRecoveryService
    {
        Task<Result<ValueObjects.PagedRecoverySkus>> GetSkusForRecovery(ValueObjects.SearchFilter searchFilter, CancellationToken cancellationToken);

        Task SendToGetAvailability(Entities.SkuRecovery skuRecovery, CancellationToken cancellationToken);
    }
}
