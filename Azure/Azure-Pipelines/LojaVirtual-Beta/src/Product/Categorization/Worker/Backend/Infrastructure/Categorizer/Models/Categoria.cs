using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Models
{
    public class Categoria
    {
        public int IdNumerico { get; set; }

        public string Nome { get; set; }

        public List<Subcategoria> Subcategorias { get; set; } = new List<Subcategoria>();
    }
}
