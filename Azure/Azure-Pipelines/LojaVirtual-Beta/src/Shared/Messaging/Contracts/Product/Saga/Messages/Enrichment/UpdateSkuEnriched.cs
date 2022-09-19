using System.Collections.Generic;

namespace Shared.Messaging.Contracts.Product.Saga.Messages.Enrichment
{
    public interface UpdateSkuEnriched
    {
        public string SkuIntegrationId { get; set; }

        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string Entity { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<int> SubcategoryIds { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public IEnumerable<string> SkuMetadata { get; set; }

        public IEnumerable<string> FiltersMetadata { get; set; }

        public string ProductHash { get; set; }

        public string ProductName { get; set; }

        public string SkuHash { get; set; }

        public string SkuName { get; set; }

        public IEnumerable<string> ProductMatchingMetadata { get; set; }

        public IEnumerable<string> ProductNameMetadata { get; set; }

        public IEnumerable<string> SkuNameMetadata { get; set; }
    }
}
