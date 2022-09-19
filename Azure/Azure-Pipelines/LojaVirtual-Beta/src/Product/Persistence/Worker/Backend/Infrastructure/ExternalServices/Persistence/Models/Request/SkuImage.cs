namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Request
{
    public class SkuImage
    {
        public string SmallImage { get; set; }

        public string MediumImage { get; set; }

        public string LargeImage { get; set; }

        public int Order { get; set; }
    }
}
