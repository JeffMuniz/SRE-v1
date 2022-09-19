namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Request
{
    public class SkuAvailability
    {
        public bool Available { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceFor { get; set; }
    }
}
