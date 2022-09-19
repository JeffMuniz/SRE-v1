using System.Collections.Generic;

namespace Shared.Messaging.Contracts.Shared.Models
{
    public class EnrichedSku
    {
        public string Name { get; set; }

        public string Hash { get; set; }

        public IDictionary<string, string> Attributes { get; set; }
    }
}
