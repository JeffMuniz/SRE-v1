using System.Collections.Generic;
using SharedUsecaseModels = Shared.Backend.Application.Usecases.Models;

namespace Product.Persistence.Worker.Backend.Application.Usecases.UpsertSku.Models
{
    public class Outbound
    {
        public string SkuId { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Ean { get; set; }

        public IEnumerable<string> Keywords { get; set; }        

        public IDictionary<string, string> SkuFeatures { get; set; }

        public string Entity { get; set; }

        public SharedUsecaseModels.Brand Brand { get; set; }

        public SharedUsecaseModels.Subcategory Subcategory { get; set; }

        public string SupplierName { get; set; }

        public string SupplierTypeId { get; set; }
    }
}
