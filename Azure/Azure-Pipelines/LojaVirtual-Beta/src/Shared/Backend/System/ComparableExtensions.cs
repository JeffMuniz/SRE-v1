using System;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class ComparableExtensions
    {
        public static bool Is<T>(this T x, T y) =>
            Equals(x, y);

        public static bool NotIs<T>(this T x, T y) =>
            !Is(x, y);

        public static bool In<T>(this T x, params T[] y) =>
            In(x, y as IEnumerable<T>);

        public static bool NotIn<T>(this T x, params T[] y) =>
            NotIn(x, y as IEnumerable<T>);

        public static bool In<T>(this T x, IEnumerable<T> y) =>
            y.Contains(x);

        public static bool NotIn<T>(this T x, IEnumerable<T> y) =>
            !In(x, y);
    }
}
