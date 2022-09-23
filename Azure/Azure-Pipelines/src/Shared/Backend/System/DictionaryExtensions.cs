using System.Collections.Generic;

namespace System.Linq
{
    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> DefaultIfNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) =>
            DefaultIfNull(dictionary, new Dictionary<TKey, TValue>(0));

        public static IDictionary<TKey, TValue> DefaultIfNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> defaultValue) =>
            dictionary is null
                ? defaultValue
                : dictionary;
    }
}
