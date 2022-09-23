using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Availability.Search.Worker.Backend.Domain.ValueObjects
{
    public class PartnerConfiguration : ValueObject
    {
        public string PartnerCode { get; init; }

        public int SupplierId { get; init; }        

        public Contract MainContract { get; init; }

        public IEnumerable<Contract> Contracts { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PartnerCode;
            yield return SupplierId;
            yield return MainContract;

            foreach (var contract in Contracts.DefaultIfNull())
                yield return contract;
        }

        public override string ToString() =>
            PartnerCode;
    }
}
