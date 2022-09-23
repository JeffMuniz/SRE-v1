using System.Collections.Generic;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static List<T> AsList<T>(this IEnumerable<T> enumerable) =>
            enumerable is List<T> list
                ? list
                : enumerable?.ToList();

        public static T[] AsArray<T>(this IEnumerable<T> enumerable) =>
            enumerable is T[] array
                ? array
                : enumerable?.ToArray();

        public static bool Empty<T>(this IEnumerable<T> enumerable) =>
            !enumerable.Any();

        public static IEnumerable<T> DefaultIfNull<T>(this IEnumerable<T> enumerable) =>
            DefaultIfNull(enumerable, Enumerable.Empty<T>());

        public static IEnumerable<T> DefaultIfNull<T>(this IEnumerable<T> enumerable, IEnumerable<T> defaultValue) =>
            enumerable is null
                ? defaultValue
                : enumerable;
    }
}
