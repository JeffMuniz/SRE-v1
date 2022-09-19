using Shared.Backend.Application.Usecases.Models;

namespace Availability.Manager.Worker.Backend.Application.UseCases.GetLatestAvailability.Models
{
    public class Outbound
    {
        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }

        public bool Available { get; set; }

        public Price Price { get; set; }
    }
}
