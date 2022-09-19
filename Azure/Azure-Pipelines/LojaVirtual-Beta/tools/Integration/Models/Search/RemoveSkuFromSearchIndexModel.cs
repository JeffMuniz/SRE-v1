using Shared.Messaging.Contracts.Product.Saga.Messages.Search;

namespace Tools.Integration.Models.Search
{
    public class RemoveSkuFromSearchIndexModel : RemoveSkuFromSearchIndex
    {
        public int SupplierId { get; set; }
        public string SupplierSkuId { get; set; }
    }
}
