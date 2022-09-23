using System.Collections.Generic;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Models
{
    public class AllContractsResult : List<ContractsConfig>
    {
    }

    public class ContractsConfig
    {
        public string Connector { get; set; }

        public string Contract { get; set; }
    }
}
