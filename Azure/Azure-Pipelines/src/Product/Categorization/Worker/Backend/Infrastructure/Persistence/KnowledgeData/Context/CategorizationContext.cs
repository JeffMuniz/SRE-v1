using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Entities;
using Shared.Persistence.Mongo;
using Shared.Persistence.Mongo.Configurations;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Context
{
    public class CategorizationContext : MongoContext<CategorizationContext, MongoOptions>, ICategorizationContext
    {
        public CategorizationContext(IOptionsMonitor<MongoOptions> settings)
            : base(settings)
        {
        }

        public IMongoCollection<Categoria> Categorias =>
            GetCollection<Categoria>("categorias");

        public IMongoCollection<Subcategoria> Subcategorias =>
            GetCollection<Subcategoria>("subcategorias");

        public IMongoCollection<ProdutoCategorizado> ProdutosCategorizados =>
            GetCollection<ProdutoCategorizado>("produtosCategorizados");
    }
}
