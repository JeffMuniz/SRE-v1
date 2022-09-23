namespace Shared.Messaging.Contracts.Product.Saga.Messages.Persistence
{
    public interface RemoveSku
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
