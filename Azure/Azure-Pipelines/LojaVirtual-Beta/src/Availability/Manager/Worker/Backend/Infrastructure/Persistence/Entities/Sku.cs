using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Entities
{
    public class Sku
    {
        [BsonId]
        [BsonRequired]
        public SkuId Id { get; set; }

        [BsonIgnoreIfDefault]
        public string PersistedSkuId { get; set; }

        [BsonIgnoreIfDefault]
        public bool MainContract { get; set; }

        [BsonIgnoreIfDefault]
        public bool Available { get; set; }

        [BsonIgnoreIfDefault]
        public Price Price { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime CreatedDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime LatestUpdatedDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime LatestPartnerAvailabilityFoundDate { get; set; }
    }
}
