using CSharpFunctionalExtensions;
using Integration.Api.Backend.Application.Offer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.MakeEnrich
{
    public interface IMakeEnrichUseCase
    {
        Task<Result> Execute(string offerId, EnrichedOfferModel enrichedOfferDto, CancellationToken cancellationToken);
    }

}
