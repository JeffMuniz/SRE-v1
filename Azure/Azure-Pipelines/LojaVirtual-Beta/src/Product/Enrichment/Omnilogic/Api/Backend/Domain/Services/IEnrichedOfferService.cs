using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Enrichment.macnaima.Api.Backend.Domain.Services
{
    public interface IEnrichedOfferService
    {
        Task<Result> NotifyUpdate(Entities.EnrichedOffer enrichedOffer, CancellationToken cancellationToken);
    }
}
