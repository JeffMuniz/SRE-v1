using Shared.Messaging.Contracts.Availability.Messages.Manager;

namespace Tools.Integration.Models.Availability
{
    public class GetLatestAvailabilityModel : GetLatestAvailability
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }

        public string ShardId { get; set; }
    }
}
