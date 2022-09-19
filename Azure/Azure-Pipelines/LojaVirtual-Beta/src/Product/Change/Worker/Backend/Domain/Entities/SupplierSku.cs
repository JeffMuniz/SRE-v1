using CSharpFunctionalExtensions;
using System.Collections.Generic;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Domain.Entities
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

        internal Result<SupplierSku, ValueObjects.ErrorType> ChangePrice(SharedDomain.ValueObjects.Price newPrice)
        {
            if (Price == newPrice)
                return ValueObjects.ErrorType.ThereIsNoChange;

            Price = newPrice;

            return this;
        }

        internal Result<SupplierSku, ValueObjects.ErrorType> ChangeActive(bool newSupplierSkuActive)
        {
            if (Active == newSupplierSkuActive)
                return ValueObjects.ErrorType.ThereIsNoChange;

            Active = newSupplierSkuActive;

            return this;
        }

        public override string ToString() =>
            $"{Id}";
    }
}
