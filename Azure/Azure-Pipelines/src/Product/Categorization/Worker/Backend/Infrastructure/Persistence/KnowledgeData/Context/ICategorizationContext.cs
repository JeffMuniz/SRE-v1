using MongoDB.Driver;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Context
{
    public interface ICategorizationContext
    {
        IMongoCollection<Entities.Categoria> Categorias { get; }

        IMongoCollection<Entities.Subcategoria> Subcategorias { get; }

        IMongoCollection<Entities.ProdutoCategorizado> ProdutosCategorizados { get; }
    }
}
