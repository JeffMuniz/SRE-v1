using Xunit;
using Xunit.Abstractions;

namespace MAc.Tests
{
    public abstract class TestBase<TFixture> : IClassFixture<TFixture>
        where TFixture : class, ITestOutputFixture
    {
        protected TestBase(TFixture fixture, ITestOutputHelper output)
        {
            Fixture = fixture;
            Fixture.CreateInjection(output);
        }

        public TFixture Fixture { get; private set; }
    }
}
