using Newtonsoft.Json;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Models
{
    public class Cursor
    {
        [JsonProperty("@hasNext")]
        public bool HasNext { get; set; }

        [JsonProperty("@nextCursorId")]
        public int Next { get; set; }

        public static Cursor Init() => 
            new() { HasNext = false, Next = 0 };
    }
}
