using CSharpFunctionalExtensions;
using Integration.Api.Backend.Application.Offer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.GetDetail
{
    public interface IGetDetailUseCase
    {
        Task<Result<OfferModel>> Execute(string offerId, CancellationToken cancellationToken);
    }

}
