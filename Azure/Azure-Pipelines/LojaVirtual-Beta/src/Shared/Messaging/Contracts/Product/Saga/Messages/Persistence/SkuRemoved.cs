namespace Shared.Messaging.Contracts.Product.Saga.Messages.Persistence
{
    public interface SkuRemoved
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
