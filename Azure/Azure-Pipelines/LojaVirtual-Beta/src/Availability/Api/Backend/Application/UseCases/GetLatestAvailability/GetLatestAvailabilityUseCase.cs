using AutoMapper;
using Availability.Api.Backend.Application.UseCases.Shared.Models;
using Availability.Api.Backend.Application.UseCases.Shared.Validations;
using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using GetLatestAvailabilityModels = Availability.Api.Backend.Application.UseCases.GetLatestAvailability.Models;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Api.Backend.Application.UseCases.GetLatestAvailability
{
    public class GetLatestAvailabilityUseCase : IGetLatestAvailabilityUseCase
    {
        private readonly IMapper _mapper;
        private readonly Domain.Services.ILatestAvailabilityService _latestAvailabilityService;

        public GetLatestAvailabilityUseCase(IMapper mapper, Domain.Services.ILatestAvailabilityService latestAvailabilityService)
        {
            _mapper = mapper;
            _latestAvailabilityService = latestAvailabilityService;
        }

        public async Task<Result<GetLatestAvailabilityModels.Outbound, SharedUsecases.Models.Error>> Execute(
            GetLatestAvailabilityModels.Inbound inbound, 
            CancellationToken cancellationToken
        )
        {
            var skuAvailabilitySearchFilter = _mapper.Map<Domain.ValueObjects.SkuAvailabilitySearchFilter>(inbound);

            var checkInputResult = skuAvailabilitySearchFilter.CheckInput();
            if (checkInputResult.IsFailure)
                return checkInputResult.Error;

            var getLatestAvailabilityResult = await _latestAvailabilityService.Get(skuAvailabilitySearchFilter, cancellationToken);
            if (getLatestAvailabilityResult.IsFailure)
                return ErrorBuilder.CreateRegisterNotFound(getLatestAvailabilityResult.Error);

            return _mapper.Map<GetLatestAvailabilityModels.Outbound>(getLatestAvailabilityResult.Value);
        }
    }
}
