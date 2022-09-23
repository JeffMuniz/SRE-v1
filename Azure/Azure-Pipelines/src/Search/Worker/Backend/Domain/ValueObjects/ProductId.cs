using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Search.Worker.Backend.Domain.ValueObjects
{
    public class ProductId : ValueObject
    {
        public string Value { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(ProductId productId)
            => productId?.Value;

        public static implicit operator ProductId(string productId)
            => new() { Value = productId };

        public override string ToString() =>
            $"{Value}";
    }
}
