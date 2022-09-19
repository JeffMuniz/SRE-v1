using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Enrichment.macnaima.Api.Backend.Application.Usecases.MakeEnrich
{
    public interface IMakeEnrichUsecase
    {
        Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken);
    }
}
