using CSharpFunctionalExtensions;

namespace Product.Change.Worker.Backend.Domain.Entities
{
    public class Category : Entity<string>
    {
        public string Name { get; private set; }

        public override string ToString() =>
            $"{Id}";
    }
}
