using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Models
{
    public class Subcategoria
    {
        public int IdNumerico { get; set; }

        public string Nome { get; set; }

        public int IdNumericoCategoria { get; set; }

        public Categoria Categoria { get; set; }

        public List<ProdutoProcessado> Produtos { get; set; } = new List<ProdutoProcessado>();
    }
}
