using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Availability.Search.Worker.Backend.Domain.ValueObjects
{
    public class Configuration : ValueObject
    {
        public IEnumerable<PartnerConfiguration> Partners { get; init; }

        public Result<PartnerConfiguration, ErrorType> GetPartner(int supplierId)
        {
            var partner = Partners
                .FirstOrDefault(x => x.SupplierId == supplierId);

            if (partner == null)
                return ErrorType.NotFound;

            return partner;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var partner in Partners.DefaultIfNull())
                yield return partner;
        }
    }
}
