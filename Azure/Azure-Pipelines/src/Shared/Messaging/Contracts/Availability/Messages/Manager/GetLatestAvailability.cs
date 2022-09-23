namespace Shared.Messaging.Contracts.Availability.Messages.Manager
{
    public interface GetLatestAvailability
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }

        public string ShardId { get; set; }
    }
}
