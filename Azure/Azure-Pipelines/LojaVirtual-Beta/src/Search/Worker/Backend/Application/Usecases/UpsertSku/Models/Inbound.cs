using System.Collections.Generic;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Search.Worker.Backend.Application.Usecases.UpsertSku.Models
{
    public class Inbound
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

        public SharedUsecases.Models.ImageSize Image { get; set; }

        public SharedUsecases.Models.Brand Brand { get; set; }

        public SharedUsecases.Models.Subcategory Subcategory { get; set; }

        public string SupplierName { get; set; }

        public string SupplierTypeId { get; set; }
    }
}
