using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Product.Saga.Worker.Saga.States.Mappings
{
    public class SkuStateMap : BsonClassMap<SkuState>
    {
        public SkuStateMap()
        {
            AutoMap();

            MapIdMember(x => x.CorrelationId);

            MapMember(c => c.FlowType).SetSerializer(new EnumSerializer<Models.SkuFlowType>(BsonType.String));
        }
    }
}
