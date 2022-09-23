using System;
using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Models
{
    public class Produto
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Marca { get; set; }

        public string CategoriaParceiro { get; set; }

        public string SubcategoriaParceiro { get; set; }

        public IEnumerable<Caracteristica> Caracteristicas { get; set; }
    }
}
