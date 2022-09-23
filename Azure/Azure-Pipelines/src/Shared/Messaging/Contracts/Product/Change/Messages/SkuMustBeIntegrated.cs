using Shared.Messaging.Contracts.Shared.Models;

namespace Shared.Messaging.Contracts.Product.Change.Messages
{
    public interface SkuMustBeIntegrated
    {
        int SupplierId { get; set; }

        string SupplierSkuId { get; set; }

        bool? Active { get; set; }

        Price Price { get; set; }
    }
}
