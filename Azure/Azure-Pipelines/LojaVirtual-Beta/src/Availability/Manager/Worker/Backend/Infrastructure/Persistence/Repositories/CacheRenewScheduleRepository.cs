using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Repositories;
using Availability.Manager.Worker.Backend.Infrastructure.Persistence.Contexts;
using CSharpFunctionalExtensions;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Repositories
{
    public class CacheRenewScheduleRepository : ICacheRenewScheduleRepository
    {
        private readonly IMapper _mapper;
        private readonly IIntegrationContext _integrationDatabase;

        public CacheRenewScheduleRepository(IMapper mapper, IIntegrationContext integrationDatabase)
        {
            _mapper = mapper;
            _integrationDatabase = integrationDatabase;
        }

        public async Task<Maybe<Domain.Entities.CacheRenewSchedule>> Get(
            Domain.ValueObjects.CacheRenewScheduleId id,
            CancellationToken cancellationToken
        )
        {
            var renewCacheScheduleId = _mapper.Map<Entities.CacheRenewScheduleId>(id);

            var renewCacheSchedule = await _integrationDatabase.CacheRenewSchedule
                .Find(schedule => schedule.Id == renewCacheScheduleId)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Domain.Entities.CacheRenewSchedule>(renewCacheSchedule);
        }

        public async Task<Domain.Entities.CacheRenewSchedule> Add(
            Domain.Entities.CacheRenewSchedule cacheRenewSchedule,
            CancellationToken cancellationToken
        )
        {
            var renewCacheScheduleDb = _mapper.Map<Entities.CacheRenewSchedule>(cacheRenewSchedule);

            await _integrationDatabase.CacheRenewSchedule.InsertOneAsync(
                renewCacheScheduleDb,
                default,
                cancellationToken
            );

            return _mapper.Map(renewCacheScheduleDb, cacheRenewSchedule);
        }

        public async Task<bool> Update(
            Domain.Entities.CacheRenewSchedule cacheRenewSchedule,
            CancellationToken cancellationToken
        )
        {
            var renewCacheScheduleDb = _mapper.Map<Entities.CacheRenewSchedule>(cacheRenewSchedule);

            var resultDb = await _integrationDatabase.CacheRenewSchedule.ReplaceOneAsync(
                schedule => schedule.Id == renewCacheScheduleDb.Id,
                renewCacheScheduleDb,
                default(ReplaceOptions),
                cancellationToken
            );

            return resultDb.ModifiedCount > 0;
        }
    }
}
