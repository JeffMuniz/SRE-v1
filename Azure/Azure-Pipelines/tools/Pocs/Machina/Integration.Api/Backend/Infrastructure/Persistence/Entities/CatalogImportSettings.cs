using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Integration.Api.Backend.Infrastructure.Persistence.Entities
{
    public class CatalogImportSettings
    {
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public int ImportedOffersCount { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime LastModifiedAt { get; set; }
    }
}
