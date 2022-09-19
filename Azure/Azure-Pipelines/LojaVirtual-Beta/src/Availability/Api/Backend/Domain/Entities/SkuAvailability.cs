using Availability.Api.Backend.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using SharedDomain = Shared.Backend.Domain;

namespace Availability.Api.Backend.Domain.Entities
{
    public class SkuAvailability : Entity<SkuAvailabilityId>
    {
        public string PersistedSkuId { get; init; }

        public bool Available { get; init; }

        public SharedDomain.ValueObjects.Price Price { get; init; }

        public override string ToString() =>
            $"{Id}";
    }
}
