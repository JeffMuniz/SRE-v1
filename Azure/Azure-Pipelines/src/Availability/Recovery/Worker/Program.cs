using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using BaseWorker = Shared.Worker;

namespace Availability.Recovery.Worker
{
    public class Program : BaseWorker.Program<Startup>
    {
        protected Program()
        {
        }

        [NoAutomaticTrigger]
        public static Task Main(string[] args) =>
            Run(args);
    }
}
