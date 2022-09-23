using System.Linq;

namespace System
{
    public static class TimeZoneInfoExtensions
    {
        private static readonly string[] BrazilianTimeZoneIds = new[] { "America/Sao_Paulo", "E. South America Standard Time" };
        private static TimeZoneInfo _cachedBrazilianTimeZone;

        public static TimeZoneInfo GetBrazilianTimeZone() =>
            _cachedBrazilianTimeZone ??=
                TimeZoneInfo
                    .GetSystemTimeZones()
                    .AsParallel()
                    .FirstOrDefault(x => BrazilianTimeZoneIds.Any(id => x.Id.Contains(id, StringComparison.InvariantCultureIgnoreCase)))
                ?? throw new TimeZoneNotFoundException(string.Join(",", BrazilianTimeZoneIds));
    }
}
