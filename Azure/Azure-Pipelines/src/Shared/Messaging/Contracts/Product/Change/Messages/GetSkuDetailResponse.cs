using Shared.Messaging.Contracts.Shared.Models;

namespace Shared.Messaging.Contracts.Product.Change.Messages
{
    public interface GetSkuDetailResponse
    {
        public string SkuIntegrationId { get; set; }

        SupplierSku SupplierSku { get; set; }
    }
}
