using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Entities
{
    public class ProdutoCategorizado
    {
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Marca { get; set; }

        public string CategoriaParceiro { get; set; }

        public string SubcategoriaParceiro { get; set; }
    
        public IEnumerable<Caracteristica> Caracteristicas { get; set; }

        public int IdSubcategoria { get; set; }

        public Subcategoria Subcategoria { get; set; }

        [BsonDateTimeOptions]
        public DateTime DataInsercao { get; set; }
    }
}
