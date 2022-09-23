using System;

namespace Availability.Recovery.Worker.Jobs
{
    public class JobScheduleConfiguration
    {
        public TimeSpan TimeLapse { get; set; }

        public string CronExpression { get; set; }

        public int PageSize { get; set; }
    }
}
