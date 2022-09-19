namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability.Models
{
    public class AvailabilityRequest
    {
        public string PartnerCode { get; set; }

        public string SupplierSkuId { get; set; }

        public string ContractId { get; set; }
    }
}
