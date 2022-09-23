using Shared.Messaging.Contracts.Shared.Models;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Tools.Integration.Models.Categorization
{
    public class CategorizeSkuModel : SagaMessages.Categorization.CategorizeSku
    {
        public string SkuIntegrationId { get; set; }

        public SupplierSku SupplierSku { get; set; }
    }
}
