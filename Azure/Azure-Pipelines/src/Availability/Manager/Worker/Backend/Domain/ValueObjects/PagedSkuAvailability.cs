using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Availability.Manager.Worker.Backend.Domain.ValueObjects
{
    public class PagedSkuAvailability : ValueObject
    {
        public int Total { get; init; }

        public IEnumerable<Entities.SkuAvailability> Skus { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Total;

            foreach (var sku in Skus.DefaultIfNull())
                yield return sku;
        }

        public override string ToString() =>
            $"{Total}, {Skus.Count()}";
    }
}
