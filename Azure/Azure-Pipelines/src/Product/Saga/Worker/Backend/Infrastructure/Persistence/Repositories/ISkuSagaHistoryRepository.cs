using System.Threading;
using System.Threading.Tasks;

namespace Product.Saga.Worker.Backend.Infrastructure.Persistence.Repositories
{
    public interface ISkuSagaHistoryRepository
    {
        Task Add(
            Saga.States.SkuState skuState,
            CancellationToken cancellationToken
        );
    }
}
