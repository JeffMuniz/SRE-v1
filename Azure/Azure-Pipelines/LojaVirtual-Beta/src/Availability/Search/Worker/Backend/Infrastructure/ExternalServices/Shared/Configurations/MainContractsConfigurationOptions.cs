using System.Collections.Generic;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Shared.Configurations
{
    public class MainContractsConfigurationOptions : List<MainContractConfiguration>
    {
    }

    public class MainContractConfiguration
    {
        public int SupplierId { get; set; }

        public string MainContract { get; set; }
    }
}
