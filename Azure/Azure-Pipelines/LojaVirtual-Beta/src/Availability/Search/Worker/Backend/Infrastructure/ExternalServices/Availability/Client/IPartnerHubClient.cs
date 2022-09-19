using CSharpFunctionalExtensions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability.Client
{
    public interface IPartnerHubClient
    {
        Task<Result<Models.AvailabilityResult, HttpStatusCode>> GetAvailability(
            Models.AvailabilityRequest availabilityRequest,
            CancellationToken cancellationToken = default
        );
    }
}
