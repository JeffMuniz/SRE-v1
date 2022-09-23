using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Entities
{    
    public class SkuIntegration
    {
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public SupplierSku SupplierSku { get; set; }

        [BsonIgnoreIfDefault]
        public string ChangedHash { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime LastModifiedAt { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime LastIntegratedAt { get; set; }
    }
}
