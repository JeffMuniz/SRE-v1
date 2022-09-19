using System;

namespace Shared.Job.Abstractions.Availability.Recovery
{
    public class AvailabilityRecoveryJobContext
    {
        public int PageSize { get; set; }

        public TimeSpan InitialTime { get; set; }

        public TimeSpan? EndTime { get; set; }
    }
}
