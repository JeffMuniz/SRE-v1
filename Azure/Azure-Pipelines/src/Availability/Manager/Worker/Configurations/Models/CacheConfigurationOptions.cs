using System;

namespace Availability.Manager.Worker.Configurations.Models
{
    public class CacheConfigurationOptions
    {        
        public int KeysPerPage { get; set; } = 1;

        public CacheResiliencyOptions Resiliency { get; set; }

        public CacheAvailabilityOptions Availability { get; set; }
    }    

    public class CacheResiliencyOptions
    {
        public int RetryAttempts { get; set; } = 1;
    }    

    public class CacheAvailabilityOptions
    {
        public CacheAvailabilityCashOptions Cash { get; set; }

        public CacheAvailabilityPointsOptions Points { get; set; }

        public CacheRenewOptions Renew { get; set; }
    }

    public class CacheAvailabilityCashOptions
    {
        public int DbNumber { get; set; }

        public TimeSpan ExpiresTime { get; set; }
    }

    public class CacheAvailabilityPointsOptions
    {
        public int DbNumber { get; set; }
    }

    public class CacheRenewOptions
    {
        public TimeSpan ScheduleTime { get; set; }

        public TimeSpan IdleTime { get; set; }
    }
}
