using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Supplier.Magalu.Worker.Backend.Domain.ValueObjects
{
    public class Specification : ValueObject
    {
        public string Name { get; init; }
        public string Value { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Value;
        }

        public override string ToString() =>
            $"{Name}|{Value}";
    }
}
