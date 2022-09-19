using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Search.Worker.Backend.Domain.Entities
{
    public class Subcategory : Entity<int>
    {
        public string Name { get; init; }

        public Category Category { get; init; }

        public override string ToString() =>
            $"{Id}|{Name}|{Category}";
    }
}
