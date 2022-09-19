using Shared.Backend.Application.Usecases.Models;

namespace Product.Change.Worker.Backend.Application.Usecases.SkuMustBeIntegrated.Models
{
    public class Inbound
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public bool? Active { get; set; }

        public Price Price { get; set; }
    }
}
