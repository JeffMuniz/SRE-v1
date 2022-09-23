namespace Shared.Messaging.Contracts.Product.Saga.Messages.Search
{
    public interface RemoveSkuFromSearchIndex
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
