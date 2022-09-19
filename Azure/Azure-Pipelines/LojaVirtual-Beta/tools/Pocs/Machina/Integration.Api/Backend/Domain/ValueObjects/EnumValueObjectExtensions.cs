using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Integration.Api.Backend.Domain.ValueObjects
{
    public static class EnumValueObjectExtensions
    {
        public static bool Is<T>(this T x, T y) where T : EnumValueObject<T> =>
            x == y;

        public static bool IsNot<T>(this T x, T y) where T : EnumValueObject<T> =>
            x != y;

        public static bool In<T>(this T x, params T[] y) where T : EnumValueObject<T> =>
            In(x, y as IEnumerable<T>);

        public static bool NotIn<T>(this T x, params T[] y) where T : EnumValueObject<T> =>
            NotIn(x, y as IEnumerable<T>);

        public static bool In<T>(this T x, IEnumerable<T> y) where T : EnumValueObject<T> =>
            y.Contains(x);

        public static bool NotIn<T>(this T x, IEnumerable<T> y) where T : EnumValueObject<T> =>
            !y.Contains(x);
    }
}
