using CSharpFunctionalExtensions;
using Product.Persistence.Worker.Backend.Domain.Entities;
using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class PersistedData : ValueObject
    {
        public string SkuId { get; init; }

        public string ProductId { get; init; }

        public string SkuName { get; init; }

        public Supplier Supplier { get; init; }

        public Brand Brand { get; init; }

        public Subcategory Subcategory { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SkuId;
            yield return ProductId;
            yield return SkuName;
            yield return Supplier;
            yield return Brand;
            yield return Subcategory;
        }

        public override string ToString() =>
            $"{SkuId}|{ProductId}";
    }
}
