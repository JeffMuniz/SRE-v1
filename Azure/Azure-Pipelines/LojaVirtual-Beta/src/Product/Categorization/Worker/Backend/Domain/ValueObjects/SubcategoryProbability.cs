using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Categorization.Worker.Backend.Domain.ValueObjects
{
    public class SubcategoryProbability : ValueObject
    {
        public int SubcategoryId { get; init; }

        public double Probability { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SubcategoryId;
            yield return Probability;
        }

        public override string ToString() =>
            $"{SubcategoryId}, {Probability}";
    }
}
