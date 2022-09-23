using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class EnrichedProduct : ValueObject
    {
        public string Entity { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Entity;
            yield return Hash;
            yield return Name;
        }

        public override string ToString() =>
            $"{Entity}|{Hash}|{Name}";
    }
}
