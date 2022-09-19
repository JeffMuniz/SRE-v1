using Microsoft.AspNetCore.Mvc;

namespace Availability.Api.Endpoints.Models
{
    public class GetPartnerAvailabilityIdModel
    {
        [FromRoute(Name = "supplierId")]
        public int SupplierId { get; init; }

        [FromRoute(Name = "supplierSkuId")]
        public string SupplierSkuId { get; init; }

        [FromRoute(Name = "contractId")]
        public string ContractId { get; init; }
    }
}
