using Shared.Messaging.Contracts.Shared.Models;

namespace Shared.Messaging.Contracts.Product.Saga.Messages.Categorization
{
    public interface CategorizeSku
    {
        public string SkuIntegrationId { get; set; }

        SupplierSku SupplierSku { get; set; }
    }
}
