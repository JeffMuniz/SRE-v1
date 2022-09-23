using CSharpFunctionalExtensions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Client
{
    public interface IPartnerHubConfigurationClient
    {
        Task<Result<Models.AllContractsResult>> GetAllContracts(CancellationToken cancellationToken = default);

        Task<Result<Models.AllPartnersResult>> GetAllPartners(CancellationToken cancellationToken = default);

        Task<Result<Models.ContractParametersResult, HttpStatusCode>> GetContractParameters(string contract, string connector, CancellationToken cancellationToken = default);
    }
}
