using System;
using Xunit.Abstractions;

namespace MAc.Tests
{
    public interface ITestOutputFixture : IDisposable
    {
        void CreateInjection(ITestOutputHelper output);
    }
}