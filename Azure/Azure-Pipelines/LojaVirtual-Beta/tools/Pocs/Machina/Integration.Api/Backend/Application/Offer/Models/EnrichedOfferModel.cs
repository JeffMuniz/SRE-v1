using Newtonsoft.Json;
using System.Collections.Generic;

namespace Integration.Api.Backend.Application.Offer.Models
{
    public class EnrichedOfferModel
    {
        [JsonProperty("store")]
        public string Store { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("seller_id")]
        public string SellerId { get; set; }

        [JsonProperty("seller_offer_id")]
        public string SellerOfferId { get; set; }

        [JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

        [JsonProperty("category_id")]
        public string CategoryId { get; set; }

        [JsonProperty("subcategory_ids")]
        public IEnumerable<string> SubcategoryIds { get; set; }

        [JsonProperty("product_hash")]
        public string ProductHash { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("sku_hash")]
        public string SkuHash { get; set; }

        [JsonProperty("sku_name")]
        public string SkuName { get; set; }

        [JsonProperty("product_matching_metadata")]
        public IEnumerable<string> ProductMatchingMetadata { get; set; }

        [JsonProperty("product_name_metadata")]
        public IEnumerable<string> ProductNameMetadata { get; set; }

        [JsonProperty("sku_metadata")]
        public IEnumerable<string> SkuMetadata { get; set; }

        [JsonProperty("sku_name_metadata")]
        public IEnumerable<string> SkuNameMetadata { get; set; }

        [JsonProperty("filters_metadata")]
        public IEnumerable<string> FiltersMetadata { get; set; }
    }
}
