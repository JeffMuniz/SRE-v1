using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Search.Worker.Backend.Domain.Services
{
    public interface IAvailabilityService
    {
        Task<Result<Entities.Availability, ValueObjects.ErrorType>> GetAvailability(ValueObjects.PartnerConfiguration partner, ValueObjects.AvailabilityId availabilityId, CancellationToken cancellationToken);
    }
}
