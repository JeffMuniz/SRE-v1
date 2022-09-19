using System.Collections.Generic;

namespace Shared.Backend.Application.Usecases.Models
{
    public class SupplierSku
    {
        public int SupplierId { get; set; }

        public string SkuId { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Ean { get; set; }

        public string Url { get; set; }

        public Price Price { get; set; }

        public Subcategory Subcategory { get; set; }

        public Brand Brand { get; set; }

        public IDictionary<string, string> Attributes { get; set; }

        public IEnumerable<Image> Images { get; set; }

        public bool Active { get; set; }
    }
}
