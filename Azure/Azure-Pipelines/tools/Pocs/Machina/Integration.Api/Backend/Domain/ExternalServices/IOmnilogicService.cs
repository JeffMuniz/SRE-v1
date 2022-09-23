using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Domain.ExternalServices
{
    public interface IMacnaimaService
    {
        Task<Result> NotifyOffer(string offerId, CancellationToken cancellationToken);

        Task<Result> NotifyOffer(string store, string offerId, CancellationToken cancellationToken);
    }
}
