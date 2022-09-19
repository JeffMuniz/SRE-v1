using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Integration.Api.Backend.Infrastructure.Persistence.Entities
{
    public class OfferNotificationHistory
    {
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public OfferNotification OfferNotification { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
