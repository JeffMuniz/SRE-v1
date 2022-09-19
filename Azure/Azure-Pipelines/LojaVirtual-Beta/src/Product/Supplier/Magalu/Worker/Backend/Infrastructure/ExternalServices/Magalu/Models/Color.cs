using Newtonsoft.Json;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Models
{
    public class Color
    {
        [JsonProperty("@strCor")]
        public string Code { get; set; }

        [JsonProperty("@strDescricao")]
        public string Description { get; set; }
    }
}
