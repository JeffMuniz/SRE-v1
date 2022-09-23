using Shared.Messaging.Contracts.Shared.Models;
using System.Collections.Generic;

namespace Product.Saga.Worker.Saga.States.Models
{
    public class PersistedSku
    {
        public string SkuId { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Ean { get; set; }

        public IEnumerable<string> Keywords { get; set; }        

        public IDictionary<string, string> SkuFeatures { get; set; }

        public string Entity { get; set; }

        public Brand Brand { get; set; }

        public Subcategory Subcategory { get; set; }

        public string SupplierName { get; set; }

        public string SupplierTypeId { get; set; }
    }
}
