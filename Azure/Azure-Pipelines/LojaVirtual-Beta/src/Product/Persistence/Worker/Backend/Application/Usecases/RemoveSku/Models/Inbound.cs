using System;

namespace Product.Persistence.Worker.Backend.Application.Usecases.RemoveSku.Models
{
    public class Inbound
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
