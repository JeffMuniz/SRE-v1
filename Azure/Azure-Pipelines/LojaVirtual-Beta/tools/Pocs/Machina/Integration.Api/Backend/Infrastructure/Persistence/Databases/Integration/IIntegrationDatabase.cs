using Integration.Api.Backend.Infrastructure.Persistence.Entities;
using MongoDB.Driver;

namespace Integration.Api.Backend.Infrastructure.Persistence.Databases.Integration
{
    public interface IIntegrationDatabase
    {
        IMongoCollection<OfferNotification> OfferNotification { get; }

        IMongoCollection<OfferNotificationHistory> OfferNotificationHistory { get; }

        IMongoCollection<CatalogImportSettings> CatalogImportSettings { get; }
    }
}
