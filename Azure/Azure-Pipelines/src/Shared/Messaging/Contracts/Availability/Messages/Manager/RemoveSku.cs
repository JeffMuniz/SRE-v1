namespace Shared.Messaging.Contracts.Availability.Messages.Manager
{
    public interface RemoveSku
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
