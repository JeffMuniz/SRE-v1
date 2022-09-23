using Newtonsoft.Json;
using Shared.Messaging.Contracts.Shared.Models;

namespace Availability.Api.Endpoints.Models
{
    public class LatestAvailabilityModel
    {
        [JsonProperty("contractId")]
        public string ContractId { get; set; }

        [JsonProperty("persistedSkuId")]
        public string PersistedSkuId { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }
    }
}
