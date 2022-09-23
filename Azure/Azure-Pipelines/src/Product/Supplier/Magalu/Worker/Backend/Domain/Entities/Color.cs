using CSharpFunctionalExtensions;

namespace Product.Supplier.Magalu.Worker.Backend.Domain.Entities
{
    public class Color : Entity<string>
    {
        public string Name { get; init; }
    }
}
