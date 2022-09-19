namespace Shared.Messaging.Contracts.Availability.Messages.Manager
{
    public interface SkuRemovedFromAvailability
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
