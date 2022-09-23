using CSharpFunctionalExtensions;
using System.Collections.Generic;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Supplier.Shared.Worker.Backend.Domain.Entities
{
    public class SupplierSku : Entity<SharedDomain.ValueObjects.SupplierSkuId>
    {
        public string ProductId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Ean { get; private set; }

        public string Url { get; private set; }

        public SharedDomain.ValueObjects.Price Price { get; private set; }

        public Subcategory Subcategory { get; private set; }

        public Brand Brand { get; private set; }        

        public IDictionary<string, string> Attributes { get; private set; }

        public IEnumerable<SharedDomain.ValueObjects.Image> Images { get; private set; }

        public bool Active { get; private set; }

        public override string ToString() =>
            $"{Id}";
    }
}
