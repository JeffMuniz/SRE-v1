using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class EnrichedSku : ValueObject
    {
        public string Hash { get; init; }
        public string Name { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
            yield return Name;
        }

        public override string ToString() =>
            $"{Hash}|{Name}";
    }
}
