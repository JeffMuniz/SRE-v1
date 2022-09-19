using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Change.Worker.Backend.Application.Usecases.SkuMustBeIntegrated
{
    public interface ISkuMustBeIntegratedUsecase
    {
        Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken);
    }
}
