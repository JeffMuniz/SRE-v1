using Shared.Messaging.Contracts.Shared.Models;

namespace Availability.Api.Backend.Application.UseCases.Shared.Models
{
    public abstract class Outbound
    {
        public string ContractId { get; set; }

        public string PersistedSkuId { get; set; }

        public bool Available { get; set; }

        public Price Price { get; set; }
    }
}
