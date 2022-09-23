using Availability.Search.Worker.Backend.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using Shared.Backend.Domain.ValueObjects;

namespace Availability.Search.Worker.Backend.Domain.Entities
{
    public class Availability : Entity<AvailabilityId>
    {
        public bool MainContract { get; private set; }

        public bool Available { get; init; }

        public Price Price { get; init; }

        public static Availability Create(AvailabilityId availabilityId) =>
            new()
            {
                Id = availabilityId
            };

        public Availability AssingnMainContract(PartnerConfiguration partner)
        {
            MainContract = partner.MainContract.Id == Id.ContractId;

            return this;
        }

        public override string ToString() =>
            $"{Id}";
    }
}
