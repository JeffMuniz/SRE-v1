using CSharpFunctionalExtensions;
using Integration.Api.Backend.Domain.ValueObjects;
using System.Collections.Generic;

namespace Integration.Api.Backend.Domain.Entities
{
    public class Offer : Entity<OfferId>
    {
        public string Product { get; set; }

        public string SkuTitle { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public IEnumerable<string> Images { get; set; }

        public decimal Price { get; set; }

        public decimal ListPrice { get; set; }

        public IDictionary<string, string> SkuAttributes { get; set; }

        public IDictionary<string, string> ProductAttributes { get; set; }

        public string Ean { get; set; }

        public bool Active { get; set; }
    }
}
