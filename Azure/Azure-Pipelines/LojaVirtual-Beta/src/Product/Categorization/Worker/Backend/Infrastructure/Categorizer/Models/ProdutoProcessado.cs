using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer.Models
{
    public class ProdutoProcessado
    {
        public List<string> Palavras { get; set; } = new List<string>();
    }
}
