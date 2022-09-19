using MongoDB.Bson.Serialization.Attributes;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Entities
{
    public class SkuId
    {
        [BsonRequired]
        public int SupplierId { get; set; }

        [BsonRequired]
        public string SupplierSkuId { get; set; }

        [BsonRequired]
        public string ContractId { get; set; }

    }
}
