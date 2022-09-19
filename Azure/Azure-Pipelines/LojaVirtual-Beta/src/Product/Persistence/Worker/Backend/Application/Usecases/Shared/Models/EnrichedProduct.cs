using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Application.Usecases.Shared.Models
{
    public class EnrichedProduct
    {
        public string Entity { get; set; }

        public int SubcategoryId { get; set; }

        public string Name { get; set; }

        public string Hash { get; set; }

        public IDictionary<string, string> Attributes { get; set; }
    }
}
