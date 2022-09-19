using System;

namespace Product.Enrichment.macnaima.Worker.Backend.Infrastructure.ExternalServices
{
    public class MacnaimaOptions
    {
        public Uri BaseAddress { get; set; }

        public string AccessKey { get; set; }

        public string DefaultStore { get; set; }
    }
}
