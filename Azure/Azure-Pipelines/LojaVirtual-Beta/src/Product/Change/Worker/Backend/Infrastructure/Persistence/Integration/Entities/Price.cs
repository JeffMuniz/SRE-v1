using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Entities
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
