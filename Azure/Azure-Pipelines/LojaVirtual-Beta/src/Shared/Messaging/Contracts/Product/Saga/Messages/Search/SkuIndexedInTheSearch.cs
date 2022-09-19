namespace Shared.Messaging.Contracts.Product.Saga.Messages.Search
{
    public interface SkuIndexedInTheSearch
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
