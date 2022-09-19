using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Models.Request
{
    public class Product
    {
        public string Id { get; set; }

        public int SupplierId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public int? SubcategoryId { get; set; }

        public IEnumerable<Feature> ProductFeatures { get; set; }

        public IEnumerable<ProductSku> ProductSkus { get; set; }

        public IEnumerable<Section> Sections { get; set; }

        public Dictionary<string, string> EnrichedProductAttributes { get; set; }        

        public EnrichedProduct EnrichedProduct { get; set; }
    }
}
