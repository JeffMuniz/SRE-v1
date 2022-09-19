using AutoMapper;
using Availability.Manager.Worker.Backend.Domain.Repositories;
using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Manager.Worker.Backend.Application.UseCases.GetUnavailableSkus
{
    public class GetUnavailableSkusUseCase : IGetUnavailableSkusUseCase
    {
        private readonly IMapper _mapper;
        private readonly ISkuRepository _skuRepository;

        public GetUnavailableSkusUseCase(
            IMapper mapper,
            ISkuRepository skuRepository
        )
        {
            _mapper = mapper;
            _skuRepository = skuRepository;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var searchFilterResult = Maybe.From(_mapper.Map<Domain.ValueObjects.SkuUnavailableSearchFilter>(inbound))
                .ToResult(Domain.ValueObjects.ErrorType.InvalidInput);
            if (searchFilterResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(searchFilterResult.Error);

            var searchFilter = searchFilterResult.Value;
            var unavailableSkus = await _skuRepository.GetUnavailable(searchFilter, cancellationToken);

            return _mapper.Map<Models.Outbound>(unavailableSkus, searchFilter);
        }
    }
}
