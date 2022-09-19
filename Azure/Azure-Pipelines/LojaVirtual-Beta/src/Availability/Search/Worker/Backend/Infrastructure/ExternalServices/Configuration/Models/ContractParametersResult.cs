using System.Collections.Generic;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Models
{
    public class ContractParametersResult
    {
        public List<string> Partners { get; set; }

        public List<Parameter> Parameters { get; set; }
    }

    public class Parameter
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
