using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Request
{
    public class ProductSku
    {
        public string ProductId { get; set; }

        public string ProductSkuId { get; set; }

        public int SkuStatusId { get; set; }

        public string Ean { get; set; }

        public int PriceFrom { get; set; }

        public int PriceFor { get; set; }

        public string Description { get; init; }

        public IEnumerable<Feature> SkuFeatures { get; set; }

        public IEnumerable<SkuImage> SkuImages { get; set; }

        public Dictionary<string, string> SupplierSkuAttributes { get; set; }

        public Dictionary<string, string> EnrichedSkuAttributes { get; set; }

        public EnrichedSku EnrichedSku { get; set; }
    }
}
