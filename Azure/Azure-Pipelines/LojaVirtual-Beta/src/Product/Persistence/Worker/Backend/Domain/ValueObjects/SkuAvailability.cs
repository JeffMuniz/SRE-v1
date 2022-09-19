using CSharpFunctionalExtensions;
using System.Collections.Generic;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class SkuAvailability : ValueObject
    {
        public int SupplierId { get; init; }

        public string SupplierSkuId { get; init; }

        public bool Available { get; init; }

        public SharedDomain.ValueObjects.Price Price { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SupplierId;
            yield return SupplierSkuId;
            yield return Available;
            yield return Price;
        }
    }
}
