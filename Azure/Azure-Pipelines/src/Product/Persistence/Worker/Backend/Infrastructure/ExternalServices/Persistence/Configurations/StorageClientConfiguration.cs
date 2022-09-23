using System;

namespace Product.Persistence.Worker.Backend.Infrastructure.ExternalServices.Persistence.Configurations
{
    public class StorageClientConfiguration
    {
        public Uri BaseAddress { get; set; }

        public AuthenticationConfiguration Authentication { get; set; }

        public Uri StoreSkuEndpoint { get; set; }

        public Uri StoreAvailabilityEndpoint { get; set; }
        
        public TimeSpan Timeout { get; set; }
    }

    public class AuthenticationConfiguration
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Uri TokenEndpoint { get; set; }
    }
}
