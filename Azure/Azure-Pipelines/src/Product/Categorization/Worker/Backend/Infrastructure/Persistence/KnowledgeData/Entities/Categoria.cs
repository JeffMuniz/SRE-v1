using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Entities
{
    public class Categoria
    {
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int IdNumerico { get; set; }

        public string Nome { get; set; }
    }
}
