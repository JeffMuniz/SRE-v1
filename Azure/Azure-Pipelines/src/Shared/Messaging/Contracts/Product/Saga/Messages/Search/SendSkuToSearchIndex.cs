using SharedModels = Shared.Messaging.Contracts.Shared.Models;
using System.Collections.Generic;

namespace Shared.Messaging.Contracts.Product.Saga.Messages.Search
{
    public interface SendSkuToSearchIndex
    {
        public int SupplierId { get; set; }

        public string SupplierSkuId { get; set; }

        public string SkuId { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Ean { get; set; }

        public IEnumerable<string> Keywords { get; set; }

        public IDictionary<string, string> SkuFeatures { get; set; }

        public string Entity { get; set; }

        public SharedModels.ImageSize Image { get; set; }

        public SharedModels.Brand Brand { get; set; }

        public SharedModels.Subcategory Subcategory { get; set; }

        public string SupplierName { get; set; }

        public string SupplierTypeId { get; set; }
    }
}
