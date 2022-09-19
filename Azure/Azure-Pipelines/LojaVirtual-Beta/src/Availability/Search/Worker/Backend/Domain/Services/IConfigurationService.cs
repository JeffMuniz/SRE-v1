using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Search.Worker.Backend.Domain.Services
{
    public interface IConfigurationService
    {
        Task<Result<ValueObjects.PartnerConfiguration, ValueObjects.ErrorType>> GetPartner(
            int supplierId,
            CancellationToken cancellationToken = default
        );
    }
}
