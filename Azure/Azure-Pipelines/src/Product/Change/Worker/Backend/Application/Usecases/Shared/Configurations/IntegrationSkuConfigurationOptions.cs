using System;

namespace Product.Change.Worker.Backend.Application.Usecases.Shared.Configurations
{
    public class IntegrationSkuConfigurationOptions
    {
        public IntegrationSkuChangesConfigurationOptions Changes { get; set; }
    }

    public class IntegrationSkuChangesConfigurationOptions
    {
        public TimeSpan TimeToCheckChangesInExistingSkus { get; set; }
    }
}
