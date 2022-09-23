using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Search.Worker.Backend.Domain.ValueObjects
{
    public class SkuId : ValueObject
    {
        public string Value { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() =>
            $"{Value}";
    }
}
