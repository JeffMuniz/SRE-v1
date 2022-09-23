using System;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Shared.Configurations
{
    public class PartnerHubConfigurationOptions
    {
        public Uri BaseAddress { get; set; }

        public Uri ContractsEndpoint { get; set; }

        public Uri PartnersEndpoint { get; set; }

        public Uri ContractParametersEndpoint { get; set; }

        public PartnerHubAvailabilityAuthenticationConfiguration Authentication { get; set; }

        public TimeSpan Timeout { get; set; }
    }

    public class PartnerHubAvailabilityAuthenticationConfiguration
    {
        public AuthenticationSubscriptionKeyConfiguration SubscriptionKey { get; set; }

        public string Basic { get; set; }
    }
}
