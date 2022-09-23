using Shared.Messaging.Contracts.Shared.Models;

namespace Shared.Messaging.Contracts.Availability.Messages.Search
{
    public interface AvailabilityFound
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }

        public bool MainContract { get; set; }

        public bool Available { get; set; }

        public Price Price { get; set; }

        public string ShardId { get; set; }
    }
}
