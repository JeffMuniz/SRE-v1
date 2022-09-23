using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Supplier.Magalu.Worker.Backend.Domain.ValueObjects
{
    public class SkuId : ValueObject
    {
        public string Code { get; init; }

        public string Model { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
            yield return Model;
        }

        public override string ToString() =>
            $"{Code}-{Model}";
    }
}
