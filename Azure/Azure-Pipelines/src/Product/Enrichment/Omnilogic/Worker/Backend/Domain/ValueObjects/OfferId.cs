using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Enrichment.Macnaima.Worker.Backend.Domain.ValueObjects
{
    public class OfferId : ValueObject
    {
        public string Value { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() =>
            $"{Value}";

        public static implicit operator string(OfferId offerId)
            => offerId?.Value;

        public static implicit operator OfferId(string id)
            => new() { Value = id };
    }
}
