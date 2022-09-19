using Shared.Messaging.Contracts.Shared.Models;

namespace Shared.Messaging.Contracts.Product.Saga.Messages.Persistence
{
    public interface UpsertSku
    {
        public SupplierSku SupplierSku { get; set; }

        public CategorizedData CategorizedData { get; set; }

        public EnrichedData EnrichedData { get; set; }
    }
}
