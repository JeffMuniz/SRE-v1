using AutoMapper;
using CSharpFunctionalExtensions;
using Search.Worker.Backend.Domain.Services;
using System.Threading;
using System.Threading.Tasks;
using SharedDomain = Shared.Backend.Domain;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Search.Worker.Backend.Application.Usecases.RemoveSku
{
    public class RemoveSkuUsecase : IRemoveSkuUsecase
    {
        private readonly IMapper _mapper;
        private readonly ISearchIndexRepository _searchIndexRepository;

        public RemoveSkuUsecase(
            IMapper mapper,
            ISearchIndexRepository searchIndexRepository
        )
        {
            _mapper = mapper;
            _searchIndexRepository = searchIndexRepository;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var supplierSkuId = _mapper.Map<SharedDomain.ValueObjects.SupplierSkuId>(inbound);
            var getSkuResult = await _searchIndexRepository.Get(supplierSkuId, cancellationToken);
            if (getSkuResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(getSkuResult.Error);

            var existingSku = getSkuResult.Value;
            var deleteSkuResult = await _searchIndexRepository.Delete(existingSku, cancellationToken);
            if (deleteSkuResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(deleteSkuResult.Error);

            return _mapper.Map<Models.Outbound>(existingSku);
        }
    }
}
