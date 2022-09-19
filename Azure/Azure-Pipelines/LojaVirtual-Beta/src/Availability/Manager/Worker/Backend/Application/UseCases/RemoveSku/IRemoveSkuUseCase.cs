using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Manager.Worker.Backend.Application.UseCases.RemoveSku
{
    public interface IRemoveSkuUseCase
    {
        Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken);
    }
}
