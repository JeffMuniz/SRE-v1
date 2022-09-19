using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Change.Worker.Backend.Domain.ValueObjects
{
    public class SkuIntegrationId : ValueObject
    {
        public string Value { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() =>
            $"{Value}";

        public static implicit operator string(SkuIntegrationId skuIntegrationId)
            => skuIntegrationId?.Value;

        public static implicit operator SkuIntegrationId(string id)
            => new() { Value = id };
    }
}
