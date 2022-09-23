using AutoMapper;
using Integration.Api.Backend.Domain.Repositories;
using Integration.Api.Backend.Infrastructure.Persistence.Databases.Integration;
using Integration.Api.Backend.Infrastructure.Persistence.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Infrastructure.Persistence.Repositories
{
    public class CatalogImportSettingsRepository : ICatalogImportSettingsRepository
    {
        private readonly IMapper _mapper;
        private readonly IIntegrationDatabase _integrationDatabase;

        public CatalogImportSettingsRepository(
            IMapper mapper,
            IIntegrationDatabase integrationDatabase
        )
        {
            _mapper = mapper;
            _integrationDatabase = integrationDatabase;
        }

        public async Task<Domain.Entities.CatalogImportSettings> Get(CancellationToken cancellationToken)
        {
            var catalogImportSettings = await _integrationDatabase.CatalogImportSettings
                .Find(_ => true)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Domain.Entities.CatalogImportSettings>(catalogImportSettings);
        }

        public async Task Save(Domain.Entities.CatalogImportSettings catalogImportSettings, CancellationToken cancellationToken)
        {
            var catalogImportSettingsDb = _mapper.Map<CatalogImportSettings>(catalogImportSettings);

            if (string.IsNullOrWhiteSpace(catalogImportSettingsDb.Id))
                catalogImportSettingsDb.Id = ObjectId.GenerateNewId().ToString();

            var result = await _integrationDatabase.CatalogImportSettings
                .ReplaceOneAsync(
                    settings => settings.Id == catalogImportSettingsDb.Id,
                    catalogImportSettingsDb,
                    new ReplaceOptions { IsUpsert = true },
                    cancellationToken
                );

            _mapper.Map(catalogImportSettingsDb, catalogImportSettings);
        }
    }
}
