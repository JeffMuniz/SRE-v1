using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Api.Backend.Domain.Services
{
    public interface IPartnerAvailabilityService
    {
        Task<Result<Entities.SkuAvailability>> Get(ValueObjects.SkuAvailabilitySearchFilter availabilitySearchFilter, CancellationToken cancellationToken);
    }
}
