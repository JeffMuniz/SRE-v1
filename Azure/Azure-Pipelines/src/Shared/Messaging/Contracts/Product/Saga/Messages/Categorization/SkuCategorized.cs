using Shared.Messaging.Contracts.Shared.Models;

namespace Shared.Messaging.Contracts.Product.Saga.Messages.Categorization
{
    public interface SkuCategorized
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public CategorizedData CategorizedData { get; set; }

    }
}
