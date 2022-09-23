using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace Availability.Manager.Worker.Backend.Domain.ValueObjects
{
    public class SkuUnavailableSearchFilter : ValueObject
    {
        private readonly int _pageSize;
        private readonly int _pageIndex;

        public DateTime InitialDateTime { get; init; }

        public DateTime EndDateTime { get; init; }

        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = value > 0 ? value : 1;
        }

        public int PageIndex
        {
            get => _pageIndex;
            init => _pageIndex = value < 0 ? 0 : value;
        }

        public bool MainContract { get; init; } = true;

        public bool Available { get; init; } = false;

        public int GetOffsetSize() =>
            PageIndex * PageSize;

        public bool IsLastPage(long total) =>
            (PageSize * (PageIndex + 1)) >= total;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InitialDateTime;
            yield return EndDateTime;
            yield return PageSize;
            yield return PageIndex;
            yield return MainContract;
            yield return Available;
        }

        public override string ToString() =>
            $"{InitialDateTime},{EndDateTime},{PageSize},{PageIndex},{MainContract},{Available}";
    }
}
