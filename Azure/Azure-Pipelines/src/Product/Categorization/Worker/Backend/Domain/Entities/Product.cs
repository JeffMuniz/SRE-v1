using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Domain.Entities
{
    public class Product : Entity<string>
    {
        public string Name { get; init; }

        public string Brand { get; init; }

        public string PartnerCategory { get; init; }

        public string PartnerSubcategory { get; init; }

        public IDictionary<string, string> Features { get; init; }

        public override string ToString() =>
            $"{Id}";
    }
}
