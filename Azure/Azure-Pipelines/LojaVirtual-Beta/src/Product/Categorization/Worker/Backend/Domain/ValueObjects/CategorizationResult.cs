using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Product.Categorization.Worker.Backend.Domain.ValueObjects
{
    public class CategorizationResult : ValueObject
    {
        public bool IsCategorized =>
            Subcategory is not null;

        public SubcategoryProbability Subcategory { get; init; }

        public double ApprovalThreshold { get; init; }

        public IEnumerable<SubcategoryProbability> TopSubcategoriesProbabilities { get; init; }

        public static CategorizationResult Create(double approvalThreshold, IEnumerable<SubcategoryProbability> subcategoryProbabilities)
        {
            var topProbabilities = subcategoryProbabilities
                .OrderByDescending(x => x.Probability)
                .Take(3);

            var subcategory = subcategoryProbabilities
                .FirstOrDefault(x => x.Probability >= approvalThreshold);

            return new CategorizationResult
            {
                Subcategory = subcategory,
                ApprovalThreshold = approvalThreshold,
                TopSubcategoriesProbabilities = topProbabilities
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return IsCategorized;
            yield return Subcategory;
            yield return ApprovalThreshold;
            foreach (var item in TopSubcategoriesProbabilities.DefaultIfNull())
                yield return item;
        }

        public override string ToString() =>
            $"{IsCategorized}, {Subcategory}";
    }
}
