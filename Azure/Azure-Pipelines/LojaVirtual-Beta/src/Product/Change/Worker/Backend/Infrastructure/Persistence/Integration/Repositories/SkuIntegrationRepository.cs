using AutoMapper;
using CSharpFunctionalExtensions;
using MongoDB.Bson;
using MongoDB.Driver;
using Product.Change.Worker.Backend.Domain.Repositories;
using Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Context;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Infrastructure.Persistence.Integration.Repositories
{
    public class SkuIntegrationRepository : ISkuIntegrationRepository
    {
        private readonly IMapper _mapper;
        private readonly IIntegrationContext _integrationDatabase;

        public SkuIntegrationRepository(
            IMapper mapper,
            IIntegrationContext integrationDatabase
        )
        {
            _mapper = mapper;
            _integrationDatabase = integrationDatabase;
        }

        public async Task<Maybe<Domain.Entities.SkuIntegration>> Get(
            SharedDomain.ValueObjects.SupplierSkuId id,
            CancellationToken cancellationToken
        )
        {
            if (Maybe.From(id).HasNoValue)
                return Maybe<Domain.Entities.SkuIntegration>.None;

            return await FirstOrDefaultAsync(
                skuIntegration =>
                    skuIntegration.SupplierSku.SupplierId == id.SupplierId &&
                    skuIntegration.SupplierSku.SkuId == id.SkuId,
                cancellationToken
            );
        }

        public async Task<Maybe<Domain.Entities.SkuIntegration>> Get(
            Domain.ValueObjects.SkuIntegrationId id,
            CancellationToken cancellationToken
        )
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return Maybe<Domain.Entities.SkuIntegration>.None;

            return await FirstOrDefaultAsync(
                skuIntegration => skuIntegration.Id == objectId.ToString(),
                cancellationToken
            );
        }

        private async Task<Maybe<Domain.Entities.SkuIntegration>> FirstOrDefaultAsync(
           Expression<Func<Entities.SkuIntegration, bool>> filter,
            CancellationToken cancellationToken
        )
        {
            var skuIntegration = await _integrationDatabase.SkuIntegration
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Domain.Entities.SkuIntegration>(skuIntegration);
        }

        public async Task<Maybe<Domain.Entities.SkuIntegration>> Add(Domain.Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken)
        {
            var skuIntegrationDb = _mapper.Map<Entities.SkuIntegration>(skuIntegration);

            await _integrationDatabase.SkuIntegration.InsertOneAsync(
                skuIntegrationDb,
                default,
                cancellationToken
            );

            return _mapper.Map(skuIntegrationDb, skuIntegration);
        }

        public async Task Update(Domain.Entities.SkuIntegration skuIntegration, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(skuIntegration.Id, out var objectId))
                return;

            var skuIntegrationDb = _mapper.Map<Entities.SkuIntegration>(skuIntegration);

            await _integrationDatabase.SkuIntegration.ReplaceOneAsync(
                notification => notification.Id == objectId.ToString(),
                skuIntegrationDb,
                default(ReplaceOptions),
                cancellationToken
            );
        }
    }
}
