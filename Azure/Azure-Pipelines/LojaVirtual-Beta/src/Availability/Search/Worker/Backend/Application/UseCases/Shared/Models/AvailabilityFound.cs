using Shared.Backend.Application.Usecases.Models;

namespace Availability.Search.Worker.Backend.Application.UseCases.Shared.Models
{
    public class AvailabilityFound
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }

        public bool MainContract { get; set; }

        public bool Available { get; set; }

        public Price Price { get; set; }
    }
}
