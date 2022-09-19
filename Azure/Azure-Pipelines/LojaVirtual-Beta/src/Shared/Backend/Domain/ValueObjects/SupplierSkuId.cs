using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Shared.Backend.Domain.ValueObjects
{
    public class SupplierSkuId : ValueObject
    {
        public int SupplierId { get; init; }

        public string SkuId { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SupplierId;
            yield return SkuId;
        }

        public override string ToString() =>
            $"{SupplierId}|{SkuId}";
    }
}
