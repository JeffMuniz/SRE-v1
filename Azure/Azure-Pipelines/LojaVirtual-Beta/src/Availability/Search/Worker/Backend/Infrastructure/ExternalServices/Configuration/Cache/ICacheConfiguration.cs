namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Configuration.Cache
{
    public interface ICacheConfiguration
    {
        public void SetClientConfiguration(Domain.ValueObjects.Configuration clientConfiguration);

        public bool TryGetClientConfiguration(out Domain.ValueObjects.Configuration clientConfiguration);

    }
}
