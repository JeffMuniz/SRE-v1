using CSharpFunctionalExtensions;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Search.Worker.Backend.Domain.ValueObjects
{
    public class SkuGroupHash : ValueObject
    {
        public string Value { get; init; }

        public static SkuGroupHash Create(
            ProductId productId,
            IDictionary<string, string> skuFeatures,
            Services.IHashProviderService hashProviderService
        ) =>
            new()
            {
                Value = hashProviderService.ComputeHash(GetHashElements(productId, skuFeatures))
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() =>
            $"{Value}";

        private static IEnumerable<object> GetHashElements(ProductId productId, IDictionary<string, string> skuFeatures)
        {
            yield return productId?.Value?.NormalizeCompare(normalizeDiacritics: false);

            if (
                skuFeatures.DefaultIfNull()
                    .FirstOrDefault(feature =>
                        FeatureType.Color.Synonyms.Contains(feature.Key.NormalizeCompare())
                    ).Value is string colorValue
            )
                yield return colorValue?.NormalizeCompare(normalizeDiacritics: false);
        }

        public static implicit operator string(SkuGroupHash skuGroupHash)
            => skuGroupHash?.Value;

        public static implicit operator SkuGroupHash(string hash)
            => new() { Value = hash };
    }
}
