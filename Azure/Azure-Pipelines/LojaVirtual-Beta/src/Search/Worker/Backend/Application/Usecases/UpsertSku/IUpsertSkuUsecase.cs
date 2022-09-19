using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Search.Worker.Backend.Application.Usecases.UpsertSku
{
    public interface IUpsertSkuUsecase
    {
        Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken);
    }
}
