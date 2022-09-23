namespace Shared.Messaging.Contracts.Availability.Messages.Search
{
    public interface GetAvailability
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }

        public string ShardId { get; set; }
    }
}
