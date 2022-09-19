using MongoDB.Bson.Serialization.Attributes;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Entities
{
    public class Caracteristica
    {
        [BsonElement("productID")]
        public string ProductId { get; set; }

        public string Nome { get; set; }

        public string Valor { get; set; }
    }
}
