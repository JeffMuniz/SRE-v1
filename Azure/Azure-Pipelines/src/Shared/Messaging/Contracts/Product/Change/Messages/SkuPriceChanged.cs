using Shared.Messaging.Contracts.Shared.Models;

namespace Shared.Messaging.Contracts.Product.Change.Messages
{
    public interface SkuPriceChanged
    {
        int SupplierId { get; set; }

        string SupplierSkuId { get; set; }

        Price Price { get; set; }
    }
}
