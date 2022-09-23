using Microsoft.AspNetCore.Mvc;

namespace Availability.Api.Endpoints.Models
{
    public class GetPartnerAvailabilityModel
    {
        [FromQuery(Name = "persistedSkuId")]
        public string PersistedSkuId { get; init; }

        [FromQuery(Name = "shardId")]
        public string ShardId { get; init; }
    }
}
