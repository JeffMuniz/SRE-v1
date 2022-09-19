using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability.Models
{
    internal class Error
    {
        [JsonPropertyName("status_code")]
        public string StatusCode { get; set; }

        [JsonPropertyName("errors")]
        public IEnumerable<string> Errors { get; set; }

        [JsonPropertyName("log_id")]
        public string LogId { get; set; }

        [JsonPropertyName("error_detail")]
        public string ErrorDetail { get; set; }

        [JsonPropertyName("third_party")]
        public string ThirdParty { get; set; }
    }
}
