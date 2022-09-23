using System;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Shared.Configurations
{
    public class PartnerHubAvailabilityOptions
    {
        public Uri BaseAddress { get; set; }

        public Uri AvailabilityEndpoint { get; set; }

        public PartnerHubAuthenticationConfiguration Authentication { get; set; }

        public TimeSpan Timeout { get; set; }
    }

    public class PartnerHubAuthenticationConfiguration
    {
        public AuthenticationSubscriptionKeyConfiguration SubscriptionKey { get; set; }
    }
}
