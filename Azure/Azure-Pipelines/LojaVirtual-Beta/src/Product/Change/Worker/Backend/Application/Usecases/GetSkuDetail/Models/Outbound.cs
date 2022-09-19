using Shared.Backend.Application.Usecases.Models;

namespace Product.Change.Worker.Backend.Application.Usecases.GetSkuDetail.Models
{
    public class Outbound : SupplierSku
    {
        public string SkuIntegrationId { get; set; }
    }
}
