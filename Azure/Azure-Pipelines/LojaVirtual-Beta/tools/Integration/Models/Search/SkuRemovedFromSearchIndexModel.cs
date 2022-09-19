using Shared.Messaging.Contracts.Product.Saga.Messages.Search;

namespace Tools.Integration.Models.Search
{
    public class SkuRemovedFromSearchIndexModel : SkuRemovedFromSearchIndex
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
