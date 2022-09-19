using AutoMapper;
using CSharpFunctionalExtensions;
using Product.Persistence.Worker.Backend.Domain.Services;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Persistence.Worker.Backend.Application.Usecases.RemoveSku
{
    public class RemoveSkuUsecase : IRemoveSkuUsecase
    {
        private readonly IMapper _mapper;
        private readonly IProductStorageService _storageService;

        public RemoveSkuUsecase(IProductStorageService storageService, IMapper mapper)
        {
            _storageService = storageService;
            _mapper = mapper;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var skuAvailabilityUnavailable = _mapper.Map<Domain.ValueObjects.SkuAvailability>(inbound);

            var storeAvailabilityResult = await _storageService.StoreAvailability(skuAvailabilityUnavailable, cancellationToken);
            if (storeAvailabilityResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(storeAvailabilityResult.Error);

            return Models.Outbound.Create();
        }
    }
}
