using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.Persistence.Worker.Backend.Domain.Entities
{
    public class Product : Entity<string>
    {
        public int SupplierId { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string Keywords { get; private set; }

        public int? SubcategoryId { get; init; }

        public IEnumerable<Section> Sections { get; init; }

        public IEnumerable<ValueObjects.Feature> Features { get; init; }

        public IDictionary<string, string> EnrichedProductAttributes { get; init; }

        public ValueObjects.EnrichedProduct EnrichedProduct { get; init; }

        public ProductSku Sku { get; init; }

        public Product AssignKeywords(IEnumerable<string> keywords)
        {
            if (keywords is not null)
                Keywords = string.Join(",", keywords);

            return this;
        }

        public override string ToString()
            => $"{Id}";
    }
}
