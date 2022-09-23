using System;

namespace Integration.Api.BackgroundServices
{
    public sealed class OfferNotificationBackgroundServiceOptions
    {
        public bool Enabled { get; set; }

        public TimeSpan IntervalTime { get; set; }

        public int DegreeOfParallelism { get; set; }
    }
}
