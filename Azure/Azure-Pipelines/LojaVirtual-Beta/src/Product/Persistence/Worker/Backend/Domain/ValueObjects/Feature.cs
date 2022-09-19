using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class Feature : ValueObject
    {
        public FeatureType FeatureType { get; init; }

        public string Name { get; init; }

        public string Value { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FeatureType;
            yield return Name;
            yield return Value;
        }

        public override string ToString() =>
            $"{FeatureType}|{Name}|{Value}";
    }
}
