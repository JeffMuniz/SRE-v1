using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Job.Abstractions.Availability.Recovery
{
    public interface IAvailabilityRecoveryRecurringJob : IJob<AvailabilityRecoveryJobContext>
    {
    }
}
