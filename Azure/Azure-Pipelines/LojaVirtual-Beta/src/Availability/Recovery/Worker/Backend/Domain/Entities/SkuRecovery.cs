using CSharpFunctionalExtensions;
using SharedDomain = Shared.Backend.Domain;

namespace Availability.Recovery.Worker.Backend.Domain.Entities
{
    public class SkuRecovery : Entity<SharedDomain.ValueObjects.SupplierSkuId>
    {
        public string ContractId { get; init; }

        public string PersistedSkuId { get; init; }

        public override string ToString() =>
            $"{Id}";
    }
}
