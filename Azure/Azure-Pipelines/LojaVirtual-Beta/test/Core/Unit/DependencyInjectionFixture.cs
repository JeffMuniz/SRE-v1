using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace mac.Tests
{
    public abstract class DependencyInjectionFixture : ITestOutputFixture
    {
        private readonly string _configJsonFile;
        protected DependencyInjectionFixture(string configJsonFile)
        {
            _configJsonFile = configJsonFile;
        }

        public IServiceCollection Services { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public IServiceProvider Provider { get; protected set; }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            //no code
        }

        private bool _created = false;
        public void CreateInjection(ITestOutputHelper output)
        {
            if (!_created)
            {
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile(_configJsonFile, true, true)
                    .Build();

                Services = new ServiceCollection();
                ConfigureServices(
                    Services
                        .AddSingleton(Configuration)
                        .AddLogging(config => config.AddXUnit(output))
                );

                Provider = Services.BuildServiceProvider();

                _created = true;
            }
        }

        #region IDisposable Support
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ExecuteDispose();
                }
                _disposed = true;
            }
        }

        ~DependencyInjectionFixture()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void ExecuteDispose()
        {
            //no code
        }
        #endregion
    }
}
