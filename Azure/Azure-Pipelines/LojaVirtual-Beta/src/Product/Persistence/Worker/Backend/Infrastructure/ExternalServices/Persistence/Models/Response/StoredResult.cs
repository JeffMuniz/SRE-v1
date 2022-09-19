namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Response
{
    public class StoredResult
    {
        public string SkuId { get; set; }

        public string ProductId { get; set; }

        public string SkuName { get; set; }

        public Supplier Supplier { get; set; }

        public Brand Brand { get; set; }

        public Subcategory Subcategory { get; set; }
    }
}
