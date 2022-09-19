using System;
using Xunit.Abstractions;

namespace mac.Tests
{
    public interface ITestOutputFixture : IDisposable
    {
        void CreateInjection(ITestOutputHelper output);
    }
}