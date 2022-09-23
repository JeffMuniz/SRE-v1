using System.Collections.Generic;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Models
{
    public class AllPartnersResult : List<PartnersConfig>
    {
    }

    public class PartnersConfig
    {
        public string Connector { get; set; }

        public string Partner { get; set; }

        public CommonParameter CommonParameters { get; set; }
    }

    public class CommonParameter
    {
        public int SupplierId { get; set; }
    }
}
