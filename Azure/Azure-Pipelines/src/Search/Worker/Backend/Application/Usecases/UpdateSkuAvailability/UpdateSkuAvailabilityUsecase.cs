using AutoMapper;
using CSharpFunctionalExtensions;
using Search.Worker.Backend.Domain.Services;
using SharedUsecases = Shared.Backend.Application.Usecases;
using System.Threading;
using System.Threading.Tasks;

namespace Search.Worker.Backend.Application.Usecases.UpdateSkuAvailability
{
    public class UpdateSkuAvailabilityUsecase : IUpdateSkuAvailabilityUsecase
    {
        private readonly IMapper _mapper;
        private readonly ISearchIndexRepository _searchIndexRepository;

        public UpdateSkuAvailabilityUsecase(
            IMapper mapper,
            ISearchIndexRepository searchIndexRepository
        )
        {
            _mapper = mapper;
            _searchIndexRepository = searchIndexRepository;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            if (!inbound.MainContract)
                return _mapper.Map<SharedUsecases.Models.Error>(Domain.ValueObjects.ErrorType.IgnoreInput);

            var skuId = _mapper.Map<Domain.ValueObjects.SkuId>(inbound);
            var availability = _mapper.Map<Domain.ValueObjects.Availability>(inbound);

            var getSkuResult = await _searchIndexRepository.Get(skuId, cancellationToken);
            if (getSkuResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(getSkuResult.Error);

            var existingSku = getSkuResult.Value;
            var changeAvailabilityResult = existingSku.ChangeAvailability(availability);
            if (changeAvailabilityResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(changeAvailabilityResult.Error);

            var updateSkuResult = await _searchIndexRepository.Update(existingSku, cancellationToken);
            if (updateSkuResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(updateSkuResult.Error);

            return _mapper.Map<Models.Outbound>(existingSku);
        }
    }
}
