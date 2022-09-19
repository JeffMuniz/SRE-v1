using Shared.Messaging.Contracts.Product.Saga.Messages.Persistence;
using Shared.Messaging.Contracts.Shared.Models;

namespace Tools.Integration.Models
{
    public class UpsertSkuModel : UpsertSku
    {
        public SupplierSku SupplierSku { get; set; }

        public CategorizedData CategorizedData { get; set; }

        public EnrichedData EnrichedData { get; set; }
    }
}
