using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Application.Usecases.Shared.Models
{
    public class EnrichedSku
    {
        public string Name { get; set; }

        public string Hash { get; set; }

        public IDictionary<string, string> Attributes { get; set; }
    }
}
