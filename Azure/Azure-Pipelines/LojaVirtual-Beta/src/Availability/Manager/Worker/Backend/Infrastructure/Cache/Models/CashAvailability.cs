using System;

namespace Availability.Manager.Worker.Backend.Infrastructure.Cache.Models
{
    public class CashAvailability
    {
        public string SupplierContractId { get; set; }

        public string SkuId { get; set; }

        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public bool Available { get; set; }

        public decimal PriceFrom { get; set; }

        public decimal PriceFor { get; set; }
    }
}
