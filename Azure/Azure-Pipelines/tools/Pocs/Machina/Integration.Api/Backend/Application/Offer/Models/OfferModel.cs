using Newtonsoft.Json;
using System.Collections.Generic;

namespace Integration.Api.Backend.Application.Offer.Models
{
    public class OfferModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("seller_id")]
        public string SellerId { get; set; }

        [JsonProperty("sku_title")]
        public string SkuTitle { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("images")]
        public IEnumerable<string> Images { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("list_price")]
        public decimal ListPrice { get; set; }

        [JsonProperty("sku_attributes")]
        public IDictionary<string, string> SkuAttributes { get; set; }

        [JsonProperty("product_attributes")]
        public IDictionary<string, string> ProductAttributes { get; set; }

        [JsonProperty("ean")]
        public string Ean { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
