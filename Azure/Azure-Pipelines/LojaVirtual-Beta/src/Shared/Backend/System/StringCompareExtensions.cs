using Humanizer;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{
    public static class StringCompareExtensions
    {
        public static string NormalizeCompare(this string text) =>
            text.NormalizeCompare(normalizeDiacritics: true);

        public static string NormalizeCompare(this string text, bool normalizeDiacritics) =>
            (normalizeDiacritics ? text.NormalizeDiacritics() : text)
                .ToLowerInvariant()
                .Dehumanize();

        public static string NormalizeDiacritics(this string text) =>
            string.Concat(text
                .Normalize(NormalizationForm.FormD)
                .ToCharArray()
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            ).Normalize(NormalizationForm.FormC);
    }
}
