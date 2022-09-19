using Shared.Messaging.Contracts.Shared.Models;

namespace Shared.Messaging.Contracts.Product.Saga.Messages.Change
{
    public interface AddSku
    {
        public string SkuIntegrationId { get; set; }

        SupplierSku SupplierSku { get; set; }
    }
}
