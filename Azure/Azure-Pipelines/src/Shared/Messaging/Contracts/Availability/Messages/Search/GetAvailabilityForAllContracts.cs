namespace Shared.Messaging.Contracts.Availability.Messages.Search
{
    public interface GetAvailabilityForAllContracts
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string PersistedSkuId { get; set; }
    }
}
