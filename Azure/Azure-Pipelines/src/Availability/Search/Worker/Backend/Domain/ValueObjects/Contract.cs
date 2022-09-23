using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Availability.Search.Worker.Backend.Domain.ValueObjects
{
    public class Contract : ValueObject
    {
        public string Id { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString() =>
            Id;
    }
}
