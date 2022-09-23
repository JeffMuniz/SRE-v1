namespace Availability.Manager.Worker.Backend.Application.UseCases.RemoveSku.Models
{
    public class Inbound
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
