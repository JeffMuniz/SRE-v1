using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Integration.Api.Backend.Infrastructure.Persistence.Entities
{
    public class OfferNotification
    {
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnoreIfDefault]
        public string Status { get; set; }

        [BsonIgnoreIfDefault]
        public Offer Offer { get; set; }

        [BsonIgnoreIfDefault]
        public EnrichedOffer EnrichedOffer { get; set; }

        [BsonIgnoreIfDefault]
        public IDictionary<string, string> Metadata { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime LastModifiedAt { get; set; }
    }
}
