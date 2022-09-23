using System.Text.Json.Serialization;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability.Models
{
    public class AvailabilityResult
    {
        [JsonPropertyName("is_available")]
        public bool Available { get; set; }

        [JsonPropertyName("regular_price")]
        public decimal PriceFrom { get; set; }

        [JsonPropertyName("selling_price")]
        public decimal PriceFor { get; set; }
    }
}
