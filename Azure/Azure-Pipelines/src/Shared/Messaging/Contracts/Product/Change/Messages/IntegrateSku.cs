using Shared.Messaging.Contracts.Shared.Models;

namespace Shared.Messaging.Contracts.Product.Change.Messages
{
    public interface IntegrateSku
    {
        SupplierSku SupplierSku { get; set; }
    }
}
