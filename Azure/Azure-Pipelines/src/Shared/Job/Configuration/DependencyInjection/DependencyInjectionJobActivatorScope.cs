using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hangfire.DependencyInjection
{
    internal class DependencyInjectionJobActivatorScope : JobActivatorScope
    {
        private readonly Lazy<IServiceScope> _scope;

        public DependencyInjectionJobActivatorScope(IServiceProvider provider)
        {
            if (provider is null) throw new ArgumentNullException(nameof(provider));

            _scope = new Lazy<IServiceScope>(() => provider.CreateScope());
        }

        public override object Resolve(Type type) =>
            _scope.Value.ServiceProvider.GetService(type);

        public override void DisposeScope() =>
            _scope.Value.Dispose();
    }
}
