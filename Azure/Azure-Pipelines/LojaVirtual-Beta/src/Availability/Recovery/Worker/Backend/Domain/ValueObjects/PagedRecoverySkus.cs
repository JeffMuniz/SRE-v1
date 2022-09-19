using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Availability.Recovery.Worker.Backend.Domain.ValueObjects
{
    public class PagedRecoverySkus : ValueObject
    {
        public int Total { get; init; }

        public bool IsLastPage { get; init; }

        public IEnumerable<Entities.SkuRecovery> Skus { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Total;
            yield return IsLastPage;

            foreach (var sku in Skus.DefaultIfNull())
                yield return sku;
        }

        public override string ToString() =>
            $"{Total}, {IsLastPage}, {Skus.Count()}";
    }
}
