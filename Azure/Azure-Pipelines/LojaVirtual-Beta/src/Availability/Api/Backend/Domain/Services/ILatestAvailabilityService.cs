using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Api.Backend.Domain.Services
{
    public interface ILatestAvailabilityService
    {
        Task<Result<Entities.SkuAvailability>> Get(ValueObjects.SkuAvailabilitySearchFilter latestAvailabilitySearchFilter, CancellationToken cancellationToken);
    }
}
