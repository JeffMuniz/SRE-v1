using AutoMapper;
using CSharpFunctionalExtensions;
using Product.Change.Worker.Backend.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Change.Worker.Backend.Application.Usecases.GetSkuDetail
{
    public class GetSkuDetailUsecase : IGetSkuDetailUsecase
    {
        private readonly IMapper _mapper;
        private readonly ISkuIntegrationRepository _skuIntegrationRepository;

        public GetSkuDetailUsecase(
            IMapper mapper,
            ISkuIntegrationRepository skuIntegrationRepository
        )
        {
            _mapper = mapper;
            _skuIntegrationRepository = skuIntegrationRepository;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var skuIntegrationId = Maybe.From(_mapper.Map<Domain.ValueObjects.SkuIntegrationId>(inbound));
            if (skuIntegrationId.HasNoValue)
                return _mapper.Map<SharedUsecases.Models.Error>(Domain.ValueObjects.ErrorType.InvalidInput);

            var skuIntegration = await _skuIntegrationRepository.Get(skuIntegrationId.GetValueOrThrow(), cancellationToken);
            if (skuIntegration.HasNoValue)
                return _mapper.Map<SharedUsecases.Models.Error>(Domain.ValueObjects.ErrorType.NotFound);

            return _mapper.Map<Models.Outbound>(skuIntegration.GetValueOrThrow());
        }
    }
}
