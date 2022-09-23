using CSharpFunctionalExtensions;
using Product.Supplier.Magalu.Worker.Backend.Application.Usecases.IntegrateFullCatalog.Models;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Supplier.Magalu.Worker.Backend.Application.Usecases.IntegrateFullCatalog
{
    public interface IIntegrateFullCatalogUsecase
    {
        Task<Result<Outbound, SharedUsecases.Models.Error>> Execute(Inbound inbound, CancellationToken cancellationToken);
    }
}
