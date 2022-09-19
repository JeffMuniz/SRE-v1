using System;

namespace Integration.Api.Backend.Infrastructure.ExternalServices
{
    public class MacnaimaServiceSettings
    {
        public Uri BaseAddress { get; set; }

        public string AccessKey { get; set; }

        public string DefaultStore { get; set; }
    }
}
