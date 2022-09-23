using Microsoft.Extensions.Caching.Memory;
using System;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Cache
{
    public class CacheConfiguration : ICacheConfiguration
    {
        private const string KEY_CLIENT_CONFIGURATION = "HUB_CLIENT_CONFIGURATION";

        private readonly IMemoryCache _cache;

        public CacheConfiguration(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void SetClientConfiguration(Domain.ValueObjects.Configuration clientConfiguration) =>
            _cache.GetOrCreate(KEY_CLIENT_CONFIGURATION,
                entry => entry
                    .SetValue(clientConfiguration)
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .Value
            );

        public bool TryGetClientConfiguration(out Domain.ValueObjects.Configuration clientConfiguration) =>
            _cache.TryGetValue(KEY_CLIENT_CONFIGURATION, out clientConfiguration);
    }
}
