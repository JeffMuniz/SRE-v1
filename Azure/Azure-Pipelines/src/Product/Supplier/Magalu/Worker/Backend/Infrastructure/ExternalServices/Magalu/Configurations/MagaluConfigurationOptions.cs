using System;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Configurations
{
    public class MagaluConfigurationOptions
    {
        public long CampaignId { get; set; }

        public MagaluProductEndpoint ProductEndpoint { get; set; }

        public Uri FeatureEndpoint { get; set; }

        public Uri ColorEndpoint { get; set; }

        public TimeSpan Timeout { get; set; }
    }
}
