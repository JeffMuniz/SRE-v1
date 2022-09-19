using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Product.Saga.Worker.Backend.Infrastructure.Persistence.Entities
{
    public class SkuSagaHistory
    {
        [BsonId]
        [BsonRequired]
        public Guid Id { get; set; }

        [BsonRequired]
        public IDictionary<string, object> Data { get; set; }

        [BsonRequired]
        [BsonDateTimeOptions]
        public DateTime CreatedDate { get; set; }
    }
}
