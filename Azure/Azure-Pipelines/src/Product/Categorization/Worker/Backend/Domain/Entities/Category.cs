using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Domain.Entities
{
    public class Category : Entity<int>
    {
        public string Name { get; init; }

        public IEnumerable<Subcategory> Subcategories { get; init; }

        public override string ToString() =>
            $"{Id}";
    }
}
