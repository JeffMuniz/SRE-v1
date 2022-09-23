namespace Availability.Search.Worker.Backend.Application.UseCases.GetAvailability.Models
{
    public class Inbound
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }
    }
}
