using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class Supplier : ValueObject
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int TypeId { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
            yield return TypeId;
        }

        public override string ToString() =>
            $"{Id}|{Name}|{TypeId}";
    }
}
