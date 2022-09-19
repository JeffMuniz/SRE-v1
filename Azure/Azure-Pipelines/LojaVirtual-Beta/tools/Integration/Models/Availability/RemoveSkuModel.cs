using Shared.Messaging.Contracts.Availability.Messages.Manager;

namespace Tools.Integration.Models.Availability
{
    public class RemoveSkuModel : RemoveSku
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
