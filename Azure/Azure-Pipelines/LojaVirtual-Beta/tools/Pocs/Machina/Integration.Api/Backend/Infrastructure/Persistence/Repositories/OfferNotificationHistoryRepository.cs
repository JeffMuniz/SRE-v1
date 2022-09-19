using AutoMapper;
using Integration.Api.Backend.Infrastructure.Persistence.Databases.Integration;
using Integration.Api.Backend.Infrastructure.Persistence.Entities;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Infrastructure.Persistence.Repositories
{
    public class OfferNotificationHistoryRepository : Domain.Repositories.IOfferNotificationHistoryRepository
    {
        private readonly IMapper _mapper;
        private readonly IIntegrationDatabase _integrationDatabase;

        public OfferNotificationHistoryRepository(
            IMapper mapper,
            IIntegrationDatabase integrationDatabase
        )
        {
            _mapper = mapper;
            _integrationDatabase = integrationDatabase;
        }
        
        public async Task Add(Domain.Entities.OfferNotificationHistory offerNotificationHistory, CancellationToken cancellationToken)
        {
            var offerNotificationHistoryDb = _mapper.Map<OfferNotificationHistory>(offerNotificationHistory);            

            await _integrationDatabase.OfferNotificationHistory.InsertOneAsync(
                offerNotificationHistoryDb,
                default(InsertOneOptions),
                cancellationToken
            );

            _mapper.Map(offerNotificationHistoryDb, offerNotificationHistory);
        }
    }
}
