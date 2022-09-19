using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Entities
{
    public class SupplierSku
    {
        [BsonIgnoreIfDefault]
        public int SupplierId { get; set; }

        [BsonIgnoreIfDefault]
        public string SkuId { get; set; }

        [BsonIgnoreIfDefault]
        public string ProductId { get; set; }

        [BsonIgnoreIfDefault]
        public string Name { get; set; }

        [BsonIgnoreIfDefault]
        public string Description { get; set; }

        [BsonIgnoreIfDefault]
        public string Ean { get; set; }

        [BsonIgnoreIfDefault]
        public string Url { get; set; }

        [BsonIgnoreIfDefault]
        public Price Price { get; set; }

        [BsonIgnoreIfDefault]
        public Subcategory Subcategory { get; set; }

        [BsonIgnoreIfDefault]
        public Brand Brand { get; set; }

        [BsonIgnoreIfDefault]
        public IDictionary<string, string> Attributes { get; set; }

        [BsonIgnoreIfDefault]
        public IEnumerable<Image> Images { get; set; }

        [BsonIgnoreIfDefault]
        public bool? Active { get; set; }
    }
}
