namespace Search.Worker.Backend.Application.Usecases.Shared.Models
{
    public abstract class Outbound
    {
        public string SupplierId { get; set; }

        public string SupplierSkuId { get; set; }
    }
}
