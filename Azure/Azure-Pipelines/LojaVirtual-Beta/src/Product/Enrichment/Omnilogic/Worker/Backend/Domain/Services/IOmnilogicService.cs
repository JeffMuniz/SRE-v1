using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Enrichment.macnaima.Worker.Backend.Domain.Services
{
    public interface IMacnaimaService
    {
        Task<Result> NotifyOffer(ValueObjects.OfferId offerId, CancellationToken cancellationToken);
    }
}
