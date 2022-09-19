using AutoMapper;
using Dapper;
using Integration.Api.Backend.Infrastructure.Persistence.Databases.Catalog;
using Integration.Api.Backend.Infrastructure.Persistence.Databases.Integration;
using Integration.Api.Backend.Infrastructure.Persistence.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Infrastructure.Persistence.Repositories
{
    public class OfferNotificationRepository : Domain.Repositories.IOfferNotificationRepository
    {
        private readonly IMapper _mapper;
        private readonly IIntegrationDatabase _integrationDatabase;
        private readonly ICatalogDatabase _catalogDatabase;

        public OfferNotificationRepository(
            IMapper mapper,
            IIntegrationDatabase integrationDatabase,
            ICatalogDatabase catalogDatabase
        )
        {
            _mapper = mapper;
            _integrationDatabase = integrationDatabase;
            _catalogDatabase = catalogDatabase;
        }

        public async Task<IEnumerable<Domain.Entities.OfferNotification>> GetNextPendings(int next, CancellationToken cancellationToken)
        {
            var offersNotification = await _integrationDatabase.OfferNotification
                .Find(notification => notification.Status == Domain.ValueObjects.NotificationStatus.PendingNotification)
                .SortBy(notification => notification.CreatedAt)
                .Limit(next)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<Domain.Entities.OfferNotification>>(offersNotification);
        }

        public async Task<IEnumerable<Domain.Entities.OfferNotification>> GetNextBatchImport(int offset, int next, CancellationToken cancellationToken)
        {
            var command = new CommandDefinition(
                commandText: "EXEC [dbo].[SP_TMP_Exportar_Skus_Macnaima] @OFFSET = @offset, @NEXT = @next",
                parameters: new { offset, next },
                commandTimeout: 180,
                commandType: System.Data.CommandType.Text,
                cancellationToken: cancellationToken
            );
            if (await _catalogDatabase.Connection.ExecuteScalarAsync<string>(command) is not string json)
                return Enumerable.Empty<Domain.Entities.OfferNotification>();

            var offersNotificationDb = JsonConvert.DeserializeObject<IEnumerable<OfferNotification>>(json);

            return _mapper.Map<IEnumerable<Domain.Entities.OfferNotification>>(offersNotificationDb);
        }

        public async Task<Domain.Entities.OfferNotification> Get(string idNotification, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(idNotification, out var objectIdNotification))
                return default;

            var offerNotification = await _integrationDatabase.OfferNotification.Find(
                notification => notification.Id == objectIdNotification.ToString()
            ).FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Domain.Entities.OfferNotification>(offerNotification);
        }

        public async Task Add(Domain.Entities.OfferNotification offerNotification, CancellationToken cancellationToken)
        {
            var offerNotificationDb = _mapper.Map<OfferNotification>(offerNotification);

            await _integrationDatabase.OfferNotification.InsertOneAsync(
                offerNotificationDb,
                default(InsertOneOptions),
                cancellationToken
            );

            _mapper.Map(offerNotificationDb, offerNotification);
        }

        public async Task BulkAdd(IEnumerable<Domain.Entities.OfferNotification> offersNotification, CancellationToken cancellationToken)
        {
            var offersNotificationDb = _mapper.Map<IEnumerable<OfferNotification>>(offersNotification);

            try
            {
                await _integrationDatabase.OfferNotification.InsertManyAsync(
                    offersNotificationDb,
                    new InsertManyOptions { BypassDocumentValidation = false, IsOrdered = false },
                    cancellationToken
                );
            }
            catch (MongoBulkWriteException<OfferNotification> ex)
            {
                if (ex.WriteErrors.Any(e => e.Category != ServerErrorCategory.DuplicateKey))
                    throw;
            }

            _mapper.Map(offersNotificationDb, offersNotification);
        }

        public async Task Update(Domain.Entities.OfferNotification offerNotification, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(offerNotification.Id, out var objectIdNotification))
                return;

            var offerNotificationDb = _mapper.Map<OfferNotification>(offerNotification);

            await _integrationDatabase.OfferNotification.ReplaceOneAsync(
                notification => notification.Id == objectIdNotification.ToString(),
                offerNotificationDb,
                default(ReplaceOptions),
                cancellationToken
            );
        }
    }
}
