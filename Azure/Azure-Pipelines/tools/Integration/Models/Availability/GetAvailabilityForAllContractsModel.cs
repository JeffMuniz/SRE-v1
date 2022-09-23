using Shared.Messaging.Contracts.Availability.Messages.Search;

namespace Tools.Integration.Models.Availability
{
    public class GetAvailabilityForAllContractsModel : GetAvailabilityForAllContracts
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string PersistedSkuId { get; set; }
    }
}
