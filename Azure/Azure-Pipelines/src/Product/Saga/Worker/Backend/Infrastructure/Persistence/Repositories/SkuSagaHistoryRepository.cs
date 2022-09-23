using AutoMapper;
using Product.Saga.Worker.Backend.Infrastructure.Persistence.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Saga.Worker.Backend.Infrastructure.Persistence.Repositories
{

    internal class SkuSagaHistoryRepository : ISkuSagaHistoryRepository
    {
        private readonly IMapper _mapper;
        private readonly IIntegrationContext _integrationDatabase;

        public SkuSagaHistoryRepository(IMapper mapper, IIntegrationContext integrationDatabase)
        {
            _mapper = mapper;
            _integrationDatabase = integrationDatabase;
        }

        public async Task Add(
            Saga.States.SkuState skuState,
            CancellationToken cancellationToken
        )
        {
            var skuSagaHistory = _mapper.Map<Entities.SkuSagaHistory>(skuState);

            await _integrationDatabase.SkuSagaHistory.InsertOneAsync(
                skuSagaHistory,
                default,
                cancellationToken
            );
        }
    }
}
