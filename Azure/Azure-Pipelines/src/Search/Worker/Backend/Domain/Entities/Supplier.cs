using CSharpFunctionalExtensions;

namespace Search.Worker.Backend.Domain.Entities
{
    public class Supplier : Entity<int>
    {
        public string Name { get; init; }

        public ValueObjects.SupplierType Type { get; init; }

        public override string ToString() =>
            $"{Id}|{Name}|{Type}";
    }
}
