using System;

namespace Hangfire.DependencyInjection
{
    public class DependencyInjectionJobActivator : JobActivator
    {
        private readonly IServiceProvider _provider;

        public DependencyInjectionJobActivator(IServiceProvider provider)
        {
            if (provider is null) throw new ArgumentNullException(nameof(provider));

            _provider = provider;
        }

        public override object ActivateJob(Type jobType) =>
            _provider.GetService(jobType);

        public override JobActivatorScope BeginScope(JobActivatorContext context) =>
            new DependencyInjectionJobActivatorScope(_provider);
    }
}
