using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Enrichment.Macnaima.Api.Backend.Domain.Entities
{
    public class EnrichedOffer : Entity<ValueObjects.OfferId>
    {
        public string Store { get; init; }

        public int SupplierId { get; init; }

        public string SkuId { get; init; }

        public string Entity { get; init; }

        public int CategoryId { get; init; }

        public IEnumerable<int> SubcategoryIds { get; init; }

        public IDictionary<string, string> Metadata { get; init; }

        public IEnumerable<string> SkuMetadata { get; init; }

        public IEnumerable<string> FiltersMetadata { get; init; }

        public string ProductHash { get; init; }

        public string ProductName { get; init; }

        public string SkuHash { get; init; }

        public string SkuName { get; init; }

        public IEnumerable<string> ProductMatchingMetadata { get; init; }

        public IEnumerable<string> ProductNameMetadata { get; init; }

        public IEnumerable<string> SkuNameMetadata { get; init; }

        public int OfferStatus { get; init; }

        public string StatusDescription { get; init; }

        public string BlockedDescription { get; init; }

        public override string ToString() =>
            $"{Id}";
    }
}
