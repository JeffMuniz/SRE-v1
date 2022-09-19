using Integration.Api.Backend.Infrastructure.Persistence.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Integration.Api.Backend.Infrastructure.Persistence.Databases.Integration
{
    public class IntegrationDatabase : IIntegrationDatabase
    {
        private readonly IMongoDatabase _database;

        public IntegrationDatabase(IOptions<IntegrationDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<OfferNotification> OfferNotification =>
            _database.GetCollection<OfferNotification>(nameof(OfferNotification));

        public IMongoCollection<OfferNotificationHistory> OfferNotificationHistory =>
           _database.GetCollection<OfferNotificationHistory>(nameof(OfferNotificationHistory));

        public IMongoCollection<CatalogImportSettings> CatalogImportSettings =>
           _database.GetCollection<CatalogImportSettings>(nameof(CatalogImportSettings));
    }
}
