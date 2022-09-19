using System;
using System.Collections.Generic;

namespace Availability.Manager.Worker.Configurations.Models
{
    public class RedisConfigurationOptions : StackExchange.Redis.Extensions.Core.Configuration.RedisConfiguration, ICloneable
    {
        public TimeSpan RetryTime { get; set; }

        public RedisShardConfigurationOptions Shards { get; set; }

        public RedisConfigurationOptions Clone()
        {
            var copy = (RedisConfigurationOptions)MemberwiseClone();

            copy.ConnectionString = ConnectionString.Clone() as string;

            return copy;
        }

        object ICloneable.Clone() =>
            Clone();
    }

    public class RedisShardConfigurationOptions
    {
        public string DefaultInstanceName { get; set; }

        public IEnumerable<RedisShardInstanceConfigurationOptions> Instances { get; set; }
    }

    public class RedisShardInstanceConfigurationOptions
    {
        public string Name { get; set; }

        public string KeyPrefix { get; set; }

        public string ConnectionString { get; set; }
    }
}
