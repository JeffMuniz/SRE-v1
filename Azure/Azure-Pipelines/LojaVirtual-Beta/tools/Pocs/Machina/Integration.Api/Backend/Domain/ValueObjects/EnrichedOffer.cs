using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Integration.Api.Backend.Domain.ValueObjects
{
    public class EnrichedOffer : ValueObject
    {
        public string Store { get; set; }

        public string SellerId { get; set; }

        public string Sku { get; set; }

        public string SellerOfferId { get; set; }

        public string Entity { get; set; }

        public IDictionary<string, string> Metadata { get; set; }

        public string CategoryId { get; set; }

        public IEnumerable<string> SubcategoryIds { get; set; }

        public string ProductHash { get; set; }

        public string ProductName { get; set; }

        public string SkuHash { get; set; }

        public string SkuName { get; set; }

        public IEnumerable<string> ProductMatchingMetadata { get; set; }        

        public IEnumerable<string> ProductNameMetadata { get; set; }

        public IEnumerable<string> SkuMetadata { get; set; }

        public IEnumerable<string> SkuNameMetadata { get; set; }

        public IEnumerable<string> FiltersMetadata { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Store;
            yield return SellerId;
            yield return Sku;
            yield return SellerOfferId;
            yield return Entity;

            yield return Metadata.Count;
            foreach (var metadata in DefaultIfNull(Metadata))
                yield return metadata;

            yield return CategoryId;

            yield return SubcategoryIds.Count();
            foreach (var subcategoryId in DefaultIfNull(SubcategoryIds))
                yield return subcategoryId;

            yield return ProductHash;
            yield return ProductName;
            yield return SkuHash;
            yield return SkuName;

            yield return ProductMatchingMetadata.Count();
            foreach (var productMatchingMetadata in DefaultIfNull(ProductMatchingMetadata))
                yield return productMatchingMetadata;

            yield return ProductNameMetadata.Count();
            foreach (var productNameMetadata in DefaultIfNull(ProductNameMetadata))
                yield return productNameMetadata;

            yield return SkuMetadata.Count();
            foreach (var skuMetadata in DefaultIfNull(SkuMetadata))
                yield return skuMetadata;

            yield return FiltersMetadata.Count();
            foreach (var filterMetadata in DefaultIfNull(FiltersMetadata))
                yield return filterMetadata;
        }

        private static IEnumerable<T> DefaultIfNull<T>(IEnumerable<T> enumerable) =>
            enumerable is null
                ? Enumerable.Empty<T>()
                : enumerable;
    }
}
