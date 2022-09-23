using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace Shared.Backend.Domain.ValueObjects
{
    public class ImageSize : ValueObject
    {
        public ImageSizeType Size { get; init; }

        public Uri Url { get; init; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Size;
            yield return Url;
        }

        public override string ToString() =>
            $"{Size}|{Url}";
    }
}
