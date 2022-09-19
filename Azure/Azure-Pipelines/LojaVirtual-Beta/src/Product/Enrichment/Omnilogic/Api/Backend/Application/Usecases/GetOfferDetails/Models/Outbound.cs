using System.Collections.Generic;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Enrichment.macnaima.Api.Backend.Application.Usecases.GetOfferDetails.Models
{
    public class Outbound
    {
        public string OfferId { get; set; }

        public string SellerId { get; set; }

        public string SkuId { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Ean { get; set; }

        public SharedUsecases.Models.Price Price { get; set; }

        public string Url { get; set; }

        public IEnumerable<string> Images { get; set; }

        public IDictionary<string, string> ProductAttributes { get; set; }

        public IDictionary<string, string> SkuAttributes { get; set; }

        public bool Active { get; set; }
    }
}
