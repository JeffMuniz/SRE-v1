using CSharpFunctionalExtensions;
using Integration.Api.Backend.Application.Offer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.Create
{
    public interface ICreateUseCase
    {
        Task<Result<string>> Execute(OfferModel offer, CancellationToken cancellationToken);
    }

}
