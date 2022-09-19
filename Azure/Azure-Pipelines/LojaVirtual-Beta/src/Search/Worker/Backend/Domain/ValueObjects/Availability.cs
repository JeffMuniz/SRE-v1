using CSharpFunctionalExtensions;
using System.Collections.Generic;
using SharedDomain = Shared.Backend.Domain;

namespace Search.Worker.Backend.Domain.ValueObjects
{
    public class Availability : ValueObject
    {
        public SharedDomain.ValueObjects.Price Price { get; init; }

        public bool Available { get; init; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Available;
            yield return Price.For;
        }

        public override string ToString() =>
            $"{Available}|{Price}";
    }
}
