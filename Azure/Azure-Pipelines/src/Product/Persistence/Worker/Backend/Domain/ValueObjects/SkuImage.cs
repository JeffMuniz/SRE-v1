using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class SkuImage : ValueObject
    {
        public int Order { get; init; }

        public string SmallImage { get; init; }

        public string MediumImage { get; init; }

        public string LargeImage { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Order;
            yield return SmallImage;
            yield return MediumImage;
            yield return LargeImage;
        }

        public override string ToString() =>
            $"{Order}|{SmallImage}|{MediumImage}|{LargeImage}";
    }
}
