using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Domain.Entities
{
    public class ProductSku : Entity<string>
    {
        public string ProductId { get; init; }

        public ValueObjects.SkuStatus SkuStatus { get; init; }

        public string Ean { get; init; }

        public int PriceFrom { get; init; }

        public int PriceFor { get; init; }

        public string Description { get; init; }

        public IEnumerable<ValueObjects.Feature> SkuFeatures { get; init; }

        public IEnumerable<ValueObjects.SkuImage> SkuImages { get; init; }

        public IDictionary<string, string> SupplierSkuAttributes { get; init; }

        public IDictionary<string, string> EnrichedSkuAttributes { get; init; }

        public ValueObjects.EnrichedSku EnrichedSku { get; init; }

        public override string ToString() => $"{Id}";

    }
}
