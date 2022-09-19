using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Entities
{
    public class Price
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.Double)]
        public decimal From { get; set; }

        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.Double)]
        public decimal For { get; set; }
    }
}
