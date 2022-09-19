using SharedUsecaseModels = Shared.Backend.Application.Usecases.Models;

namespace Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku.Models
{
    public class Inbound
    {
        public string SkuIntegrationId { get; set; }

        public SharedUsecaseModels.SupplierSku SupplierSku { get; set; }
    }
}
