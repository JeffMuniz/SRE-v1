using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BaseWorker = Shared.Worker;

namespace Price.History.Worker
{
    public class Startup : BaseWorker.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
