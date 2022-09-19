using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Domain.Entities
{
    public class Brand : Entity<long>
    {
        public string Name { get; init; }

        public override string ToString() =>
            $"{Id}|{Name}";
    }
}
