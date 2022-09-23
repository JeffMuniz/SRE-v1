using System;

namespace Product.Enrichment.Macnaima.Worker.Backend.Infrastructure.ExternalServices
{
    public class MacnaimaConfigurationOptions
    {
        public Uri BaseAddress { get; set; }

        public string AccessKey { get; set; }

        public string DefaultStore { get; set; }
    }
}
