namespace Shared.Messaging.Contracts.Availability.Models.Manager
{
    public class UnavailableSku
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }
    }
}
