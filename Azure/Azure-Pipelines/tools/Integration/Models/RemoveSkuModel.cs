using Shared.Messaging.Contracts.Product.Saga.Messages.Persistence;

namespace Tools.Integration.Models
{
    public class RemoveSkuModel : RemoveSku
    {
        public int SupplierId { get; set; }
        public string SupplierSkuId { get; set; }
    }
}
