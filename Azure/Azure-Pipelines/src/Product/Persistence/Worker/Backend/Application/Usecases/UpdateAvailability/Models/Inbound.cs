using SharedUsecases = Shared.Backend.Application.Usecases.Models;

namespace Product.Persistence.Worker.Backend.Application.Usecases.UpdateAvailability.Models
{
    public class Inbound
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string PersistedSkuId { get; set; }

        public bool MainContract { get; set; }

        public bool Available { get; set; }

        public SharedUsecases.Price Price { get; set; }
    }
}
