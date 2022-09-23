using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Product.Saga.Worker.Saga.States.Mappings
{
    public class PriceMap : BsonClassMap<Shared.Messaging.Contracts.Shared.Models.Price>
    {
        public PriceMap()
        {
            AutoMap();

            MapMember(x => x.From)
                .SetSerializer(new MongoDB.Bson.Serialization.Serializers.CharSerializer(BsonType.Double));

            MapMember(x => x.For)
                .SetSerializer(new MongoDB.Bson.Serialization.Serializers.CharSerializer(BsonType.Double));
        }
    }
}
