using Microsoft.AspNetCore.Mvc;

namespace Product.Enrichment.macnaima.Api.Endpoints.Models
{
    public class OfferIdModel
    {
        [FromRoute(Name = "id")]
        public string OfferId { get; set; }
    }
}
