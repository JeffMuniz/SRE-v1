using CSharpFunctionalExtensions;
using System.Collections.Generic;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Enrichment.macnaima.Api.Backend.Domain.Entities
{
    public class Offer : Entity<ValueObjects.OfferId>
    {
        public int SupplierId { get; set; }

        public string SkuId { get; set; }

        public string ProductId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Ean { get; private set; }

        public string Url { get; private set; }

        public SharedDomain.ValueObjects.Price Price { get; private set; }        

        public IEnumerable<string> Images { get; private set; }

        public IDictionary<string, string> ProductAttributes { get; private set; }

        public IDictionary<string, string> SkuAttributes { get; private set; }

        public bool Active { get; private set; }

        public override string ToString() =>
            $"{Id}";
    }
}
