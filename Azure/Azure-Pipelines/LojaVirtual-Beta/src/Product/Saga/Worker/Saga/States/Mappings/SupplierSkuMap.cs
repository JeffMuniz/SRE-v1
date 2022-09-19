using MongoDB.Bson.Serialization;
using System;

namespace Product.Saga.Worker.Saga.States.Mappings
{
    public class SupplierSkuMap : BsonClassMap<Shared.Messaging.Contracts.Shared.Models.SupplierSku>
    {
        public SupplierSkuMap()
        {
            AutoMap();
        }
    }
}
