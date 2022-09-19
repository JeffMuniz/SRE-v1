using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Repositories;
using Availability.Manager.Worker.Backend.Infrastructure.Persistence.Contexts;
using CSharpFunctionalExtensions;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using SharedValueObjects = Shared.Backend.Domain.ValueObjects;

namespace Availability.Manager.Worker.Backend.Infrastructure.Persistence.Repositories
{
    public class SkuRepository : ISkuRepository
    {
        private readonly IMapper _mapper;
        private readonly IIntegrationContext _integrationDatabase;

        public SkuRepository(IMapper mapper, IIntegrationContext integrationDatabase)
        {
            _mapper = mapper;
            _integrationDatabase = integrationDatabase;
        }

        public async Task<Maybe<Domain.Entities.SkuAvailability>> Get(
            Domain.ValueObjects.SkuAvailabilityId id,
            CancellationToken cancellationToken
        )
        {
            var skuId = _mapper.Map<Entities.SkuId>(id);

            var sku = await _integrationDatabase.Sku
                .Find(sku => sku.Id == skuId)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Domain.Entities.SkuAvailability>(sku);
        }

        public async Task<Maybe<Domain.Entities.SkuAvailability>> Get(
            SharedValueObjects.SupplierSkuId supplierSkuId,
            CancellationToken cancellationToken
        )
        {
            var sku = await _integrationDatabase.Sku
                .Find(sku =>
                    sku.Id.SupplierId == supplierSkuId.SupplierId &&
                    sku.Id.SupplierSkuId == supplierSkuId.SkuId
                )
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Domain.Entities.SkuAvailability>(sku);
        }

        public async Task<Domain.ValueObjects.PagedSkuAvailability> GetUnavailable(
            Domain.ValueObjects.SkuUnavailableSearchFilter searchFilter,
            CancellationToken cancellationToken
        )
        {
            var find = _integrationDatabase.Sku
                .Find(sku =>
                    sku.LatestUpdatedDate >= searchFilter.InitialDateTime &&
                    sku.LatestUpdatedDate <= searchFilter.EndDateTime &&
                    sku.MainContract == searchFilter.MainContract &&
                    sku.Available == searchFilter.Available
                );

            var total = await find
                .CountDocumentsAsync(cancellationToken);

            var skus = await find
                .Skip(searchFilter.GetOffsetSize())
                .Limit(searchFilter.PageSize)
                .ToListAsync(cancellationToken);

            return _mapper.Map<Domain.ValueObjects.PagedSkuAvailability>(skus, total);
        }

        public async Task<Domain.Entities.SkuAvailability> Add(
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var skuDb = _mapper.Map<Entities.Sku>(skuAvailability);

            await _integrationDatabase.Sku.InsertOneAsync(
                skuDb,
                default,
                cancellationToken
            );

            return _mapper.Map(skuDb, skuAvailability);
        }

        public async Task<bool> Update(
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var skuDb = _mapper.Map<Entities.Sku>(skuAvailability);

            var resultDb = await _integrationDatabase.Sku.ReplaceOneAsync(
                sku => sku.Id == skuDb.Id,
                skuDb,
                default(ReplaceOptions),
                cancellationToken
            );

            return resultDb.ModifiedCount > 0;
        }

        public async Task<bool> UpdateOnlyLatestPartnerAvailabilityFoundDate(
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var skuIdDb = _mapper.Map<Entities.SkuId>(skuAvailability.Id);

            var resultDb = await _integrationDatabase.Sku.UpdateOneAsync(
                sku => sku.Id == skuIdDb,
                Builders<Entities.Sku>.Update
                    .Set(sku => sku.LatestPartnerAvailabilityFoundDate, skuAvailability.LatestPartnerAvailabilityFoundDate),
                default,
                cancellationToken
            );

            return resultDb.ModifiedCount > 0;
        }

        public async Task<bool> RemoveAllContracts(
            Domain.Entities.SkuAvailability skuAvailability,
            CancellationToken cancellationToken
        )
        {
            var resultDb = await _integrationDatabase.Sku.DeleteManyAsync(
                sku =>
                    sku.Id.SupplierId == skuAvailability.Id.SupplierId &&
                    sku.Id.SupplierSkuId == skuAvailability.Id.SupplierSkuId,
                default(DeleteOptions),
                cancellationToken
            );

            return resultDb.DeletedCount > 0;
        }
    }
}
