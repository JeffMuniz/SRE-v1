using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Integration.Api.Backend.Infrastructure.Persistence.Entities
{
    public class Offer
    {
        [BsonIgnoreIfDefault]
        public string Product { get; set; }

        [BsonIgnoreIfDefault]
        public string Sku { get; set; }

        [BsonIgnoreIfDefault]
        public string SellerId { get; set; }

        [BsonIgnoreIfDefault]
        public string SkuTitle { get; set; }

        [BsonIgnoreIfDefault]
        public string Description { get; set; }

        [BsonIgnoreIfDefault]
        public string Url { get; set; }

        [BsonIgnoreIfDefault]
        public IEnumerable<string> Images { get; set; }

        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }

        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.Double)]
        public decimal ListPrice { get; set; }

        [BsonIgnoreIfDefault]
        public IDictionary<string, string> SkuAttributes { get; set; }

        [BsonIgnoreIfDefault]
        public IDictionary<string, string> ProductAttributes { get; set; }

        [BsonIgnoreIfDefault]
        public string Ean { get; set; }

        [BsonIgnoreIfDefault]
        public bool? Active { get; set; }
    }
}
