using Hangfire.Common;
using Hangfire.States;

namespace Shared.Job.Configuration.Filters
{
    public class PreserveOriginalQueueAttribute : JobFilterAttribute, IElectStateFilter
    {
        internal static class JobParam
        {
            public static readonly string OriginalQueue = nameof(OriginalQueue);
        }

        public void OnStateElection(ElectStateContext context)
        {
            if (context.CandidateState is not EnqueuedState enqueuedState)
                return;

            var queue = context.Connection.GetJobParameter(context.BackgroundJob.Id, JobParam.OriginalQueue);

            if (!string.IsNullOrWhiteSpace(queue))
            {
                enqueuedState.Queue = queue;
                return;
            }

            context.Connection.SetJobParameter(context.BackgroundJob.Id, JobParam.OriginalQueue, enqueuedState.Queue);
        }
    }
}
