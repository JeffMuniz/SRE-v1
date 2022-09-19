using Shared.Messaging.Contracts.Shared.Models;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;

namespace Tools.Integration.Models
{
    public class SkuMustBeIntegratedModel : ChangeMessages.SkuMustBeIntegrated
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public bool? Active { get; set; }

        public Price Price { get; set; }        
    }
}
