using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Integration.Api.Backend.Infrastructure.Persistence.Entities
{
    public class EnrichedOffer
    {
        [BsonIgnoreIfDefault]
        public string Store { get; set; }

        [BsonIgnoreIfDefault]
        public string Sku { get; set; }

        [BsonIgnoreIfDefault]
        public string SellerId { get; set; }

        [BsonIgnoreIfDefault]
        public string SellerOfferId { get; set; }

        [BsonIgnoreIfDefault]
        public string Entity { get; set; }

        [BsonIgnoreIfDefault]
        public IDictionary<string, string> Metadata { get; set; }

        [BsonIgnoreIfDefault]
        public string CategoryId { get; set; }

        [BsonIgnoreIfDefault]
        public IEnumerable<string> SubcategoryIds { get; set; }

        [BsonIgnoreIfDefault]
        public string ProductHash { get; set; }

        [BsonIgnoreIfDefault]
        public string ProductName { get; set; }

        [BsonIgnoreIfDefault]
        public string SkuHash { get; set; }

        [BsonIgnoreIfDefault]
        public string SkuName { get; set; }

        [BsonIgnoreIfDefault]
        public IEnumerable<string> ProductMatchingMetadata { get; set; }

        [BsonIgnoreIfDefault]
        public IEnumerable<string> ProductNameMetadata { get; set; }

        [BsonIgnoreIfDefault]
        public IEnumerable<string> SkuMetadata { get; set; }

        [BsonIgnoreIfDefault]
        public IEnumerable<string> SkuNameMetadata { get; set; }

        [BsonIgnoreIfDefault]
        public IEnumerable<string> FiltersMetadata { get; set; }
    }
}
