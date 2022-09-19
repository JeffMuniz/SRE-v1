using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class StringComparableExtensions
    {
        public static bool Is(this string x, StringComparison stringComparison, string y) =>
            string.Equals(x, y, stringComparison);

        public static bool NotIs(this string x, StringComparison stringComparison, string y) =>
            !Is(x, stringComparison, y);

        public static bool In(this string x, StringComparison stringComparison, params string[] y) =>
            In(x, stringComparison, y as IEnumerable<string>);

        public static bool NotIn(this string x, StringComparison stringComparison, params string[] y) =>
            NotIn(x, stringComparison, y as IEnumerable<string>);

        public static bool In(this string x, StringComparison stringComparison, IEnumerable<string> y) =>
            y.Any(item => item.Is(stringComparison, x));

        public static bool NotIn(this string x, StringComparison stringComparison, IEnumerable<string> y) =>
            !In(x, stringComparison, y);
    }
}
