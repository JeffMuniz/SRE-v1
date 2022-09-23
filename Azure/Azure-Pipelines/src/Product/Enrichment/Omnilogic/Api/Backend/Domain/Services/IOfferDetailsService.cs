using CSharpFunctionalExtensions;
using Product.Enrichment.Macnaima.Api.Backend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Enrichment.Macnaima.Api.Backend.Domain.Services
{
    public interface IOfferDetailsService
    {
        Task<Result<Offer>> Get(ValueObjects.OfferId offerId, CancellationToken cancellationToken);
    }
}
