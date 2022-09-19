using CSharpFunctionalExtensions;

namespace Product.Supplier.Shared.Worker.Backend.Domain.Entities
{
    public class Subcategory : Entity<string>
    {
        public string Name { get; private set; }

        public Category Category { get; private set; }

        public override string ToString() =>
            $"{Id}";
    }
}
