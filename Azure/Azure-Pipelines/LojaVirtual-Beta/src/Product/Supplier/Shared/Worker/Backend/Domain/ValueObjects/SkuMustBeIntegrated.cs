using CSharpFunctionalExtensions;
using System.Collections.Generic;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Supplier.Shared.Worker.Backend.Domain.ValueObjects
{
    public class SkuMustBeIntegrated : ValueObject
    {
        public SharedDomain.ValueObjects.SupplierSkuId SupplierSkuId { get; init; }

        public bool? Active { get; init; }

        public SharedDomain.ValueObjects.Price Price { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SupplierSkuId;
            yield return Active;
            yield return Price;
        }

        public override string ToString() =>
            $"{SupplierSkuId}|{Active}|{Price}";
    }
}
