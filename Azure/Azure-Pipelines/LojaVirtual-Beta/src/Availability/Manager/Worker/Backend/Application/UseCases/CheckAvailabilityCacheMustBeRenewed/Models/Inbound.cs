namespace Availability.Manager.Worker.Backend.Application.UseCases.CheckAvailabilityCacheMustBeRenewed.Models
{
    public class Inbound
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }

        public string ShardId { get; set; }
    }
}
