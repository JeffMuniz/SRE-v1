using Shared.Messaging.Contracts.Shared.Models;
using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Tools.Integration.Models.Categorization
{
    public class SkuCategorizedModel : SagaMessages.Categorization.SkuCategorized
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public CategorizedData CategorizedData { get; set; }
    }
}
