using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Integration.Api.Backend.Domain.ValueObjects
{
    public class OfferId : ValueObject
    {
        public string SellerId { get; }

        public string Sku { get; }

        public OfferId(string sellerId, string sku)
        {
            SellerId = sellerId;
            Sku = sku;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Sku;
            yield return SellerId;
        }

        public override string ToString() =>
            $"{SellerId}, {Sku}";
    }
}
