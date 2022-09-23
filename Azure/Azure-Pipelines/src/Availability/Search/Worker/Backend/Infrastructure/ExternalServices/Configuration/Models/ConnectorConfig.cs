using System.Collections.Generic;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Models
{
    public class ConnectorConfig
    {
        public string Connector { get; set; }

        public IEnumerable<PartnerConfig> PartnerConfigs { get; set; }
    }
}
