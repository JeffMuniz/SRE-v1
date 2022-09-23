using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using SharedDomain = Shared.Backend.Domain;

namespace Search.Worker.Backend.Domain.Entities
{
    public class Sku : Entity<ValueObjects.SkuId>
    {
        public SharedDomain.ValueObjects.SupplierSkuId SupplierSkuId { get; init; }

        public ValueObjects.SkuGroupHash Group { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string Relevance { get; init; }

        public IEnumerable<string> Keywords { get; init; }

        public string Ean { get; init; }

        public Supplier Supplier { get; init; }

        public Brand Brand { get; init; }

        public Subcategory Subcategory { get; init; }

        public ValueObjects.Availability Availability { get; private set; }

        public SharedDomain.ValueObjects.ImageSize Image { get; init; }

        public ValueObjects.ProductType Type { get; init; }

        public IDictionary<string, string> Features { get; set; }

        public string ServicePath { get; init; }

        public DateTime CreatedDate { get; init; } = DateTime.Now;

        public override string ToString()
            => $"{Id}|{Name}";

        public Result<Sku, ValueObjects.ErrorType> ChangeAvailability(ValueObjects.Availability availability)
        {
            if (Availability == availability)
                return ValueObjects.ErrorType.ThereIsNoChange;

            Availability = availability;

            return this;
        }
    }
}
