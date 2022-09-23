using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Tools.Integration.Models.Search
{
    public class SkuIndexedInTheSearchModel : SagaMessages.Search.SkuIndexedInTheSearch
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
