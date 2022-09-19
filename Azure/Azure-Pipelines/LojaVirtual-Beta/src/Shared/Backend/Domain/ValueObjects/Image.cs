using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Backend.Domain.ValueObjects
{
    public class Image : ValueObject
    {
        public int Order { get; init; }

        public IEnumerable<ImageSize> Sizes { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Order;

            foreach (var size in Sizes.DefaultIfNull())
            {
                yield return size?.Size;
                yield return size?.Url;
            }
        }

        public override string ToString() =>
            $"{Order}|{GetFormattedImageSizes()}";

        private string GetFormattedImageSizes() =>
            string.Join(",", Sizes.Select(size => size.Size.Id));
    }
}
