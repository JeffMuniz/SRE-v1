using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace Availability.Recovery.Worker.Backend.Domain.ValueObjects
{
    public class SearchFilter : ValueObject
    {
        public DateTime InitialDateTime { get; init; }

        public DateTime EndDateTime { get; init; }

        public int PageSize { get; init; }

        public int PageIndex { get; init; }

        public static SearchFilter CreateIncremented(SearchFilter request)
            => new SearchFilter
            {
                InitialDateTime = request.InitialDateTime,
                EndDateTime = request.EndDateTime,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex + 1
            };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InitialDateTime;
            yield return EndDateTime;
            yield return PageSize;
            yield return PageIndex;
        }

        public override string ToString() =>
            $"{InitialDateTime},{EndDateTime},{PageSize},{PageIndex}";
    }
}
