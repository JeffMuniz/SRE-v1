using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Repositories;
using Availability.Manager.Worker.Backend.Domain.Services;
using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;
using SharedValueObjects = Shared.Backend.Domain.ValueObjects;

namespace Availability.Manager.Worker.Backend.Application.UseCases.RemoveSku
{
    public class RemoveSkuUseCase : IRemoveSkuUseCase
    {
        private readonly IMapper _mapper;
        private readonly ISkuRepository _skuRepository;
        private readonly ICashCacheService _cashCacheService;
        private readonly IPointCacheService _pointCacheService;

        public RemoveSkuUseCase(
            IMapper mapper,
            ISkuRepository skuRepository,
            ICashCacheService cashCacheService,
            IPointCacheService pointCacheService)
        {
            _mapper = mapper;
            _skuRepository = skuRepository;
            _cashCacheService = cashCacheService;
            _pointCacheService = pointCacheService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var supplierSkuIdResult = Maybe.From(_mapper.Map<SharedValueObjects.SupplierSkuId>(inbound))
                .ToResult(Domain.ValueObjects.ErrorType.InvalidInput);
            if (supplierSkuIdResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(supplierSkuIdResult.Error);

            var supplierSkuId = supplierSkuIdResult.Value;

            var getSkuAvailabilityResult = await _skuRepository.Get(supplierSkuId, cancellationToken)
                .ToResult(Domain.ValueObjects.ErrorType.NotFound);
            if (getSkuAvailabilityResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(getSkuAvailabilityResult.Error);

            var existingSkuAvailability = getSkuAvailabilityResult.Value;

            await _skuRepository.RemoveAllContracts(existingSkuAvailability, cancellationToken);

            await _cashCacheService.RemoveAllContracts(Domain.ValueObjects.ShardId.AllShards, existingSkuAvailability, cancellationToken);

            await _pointCacheService.RemoveAllContracts(Domain.ValueObjects.ShardId.AllShards, existingSkuAvailability, cancellationToken);

            return Models.Outbound.Create();
        }
    }
}
