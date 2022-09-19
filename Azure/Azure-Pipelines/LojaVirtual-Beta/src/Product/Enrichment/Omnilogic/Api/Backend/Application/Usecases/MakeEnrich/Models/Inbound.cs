using System.Collections.Generic;

namespace Product.Enrichment.macnaima.Api.Backend.Application.Usecases.MakeEnrich.Models
{
    public class Inbound
    {
        public string OfferId { get; set; }

        public string Store { get; set; }

        public string SellerId { get; set; }

        public string SkuId { get; set; }        

        public string Entity { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public string CategoryId { get; set; }

        public IEnumerable<string> SubcategoryIds { get; set; }

        public string ProductHash { get; set; }

        public string ProductName { get; set; }

        public string SkuHash { get; set; }

        public string SkuName { get; set; }

        public IEnumerable<string> ProductMatchingMetadata { get; set; }

        public IEnumerable<string> ProductNameMetadata { get; set; }

        public IEnumerable<string> SkuMetadata { get; set; }

        public IEnumerable<string> SkuNameMetadata { get; set; }

        public IEnumerable<string> FiltersMetadata { get; set; }

        public int OfferStatus { get; set; }

        public string StatusDescription { get; set; }

        public string BlockedDescription { get; set; }
    }
}
