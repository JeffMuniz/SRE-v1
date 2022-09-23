using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Shared.Backend.Domain.ValueObjects
{
    public class Price : ValueObject
    {
        public decimal From { get; init; }

        public decimal For { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return From;
            yield return For;
        }

        public override string ToString() =>
            $"{From}|{For}";
    }
}
