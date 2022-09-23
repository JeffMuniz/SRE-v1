using System.Threading;
using System.Threading.Tasks;

namespace Product.Change.Worker.Backend.Domain.Services
{
    public interface ISkuNotificationService
    {
        Task NotifyChangedPrice(Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken);

        Task NotifyAddSku(Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken);

        Task NotifyUpdateSku(Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken);

        Task NotifyRemoveSku(Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken);
    }
}
