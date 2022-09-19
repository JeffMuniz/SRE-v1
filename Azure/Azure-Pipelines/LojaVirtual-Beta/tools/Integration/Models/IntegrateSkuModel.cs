using Shared.Messaging.Contracts.Shared.Models;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;

namespace Tools.Integration.Models
{
    public class IntegrateSkuModel : ChangeMessages.IntegrateSku
    {
        public SupplierSku SupplierSku { get; set; }
    }
}
