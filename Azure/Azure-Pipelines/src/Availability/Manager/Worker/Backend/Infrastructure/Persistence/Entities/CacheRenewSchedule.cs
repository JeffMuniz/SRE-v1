using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Entities
{
    public class CacheRenewSchedule
    {
        [BsonId]
        [BsonRequired]
        public CacheRenewScheduleId Id { get; set; }

        [BsonIgnoreIfDefault]
        public Guid ScheduleId { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime ScheduleDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime CreatedDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonDateTimeOptions]
        public DateTime LatestUpdatedDate { get; set; }
    }
}
