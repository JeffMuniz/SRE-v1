using AutoMapper;
using Availability.Api.Backend.Application.UseCases.Shared.Models;
using Availability.Api.Backend.Application.UseCases.Shared.Validations;
using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using GetPartnerAvailabilityModels = Availability.Api.Backend.Application.UseCases.GetPartnerAvailability.Models;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Api.Backend.Application.UseCases.GetPartnerAvailability
{
    public class GetPartnerAvailabilityUseCase : IGetPartnerAvailabilityUseCase
    {
        private readonly IMapper _mapper;
        private readonly Domain.Services.IPartnerAvailabilityService _partnerAvailabilityService;

        public GetPartnerAvailabilityUseCase(IMapper mapper, Domain.Services.IPartnerAvailabilityService partnerAvailabilityService)
        {
            _mapper = mapper;
            _partnerAvailabilityService = partnerAvailabilityService;
        }

        public async Task<Result<GetPartnerAvailabilityModels.Outbound, SharedUsecases.Models.Error>> Execute(
            GetPartnerAvailabilityModels.Inbound inbound,
            CancellationToken cancellationToken
        )
        {
            var skuAvailabilitySearchFilter = _mapper.Map<Domain.ValueObjects.SkuAvailabilitySearchFilter>(inbound);

            var checkInputResult = skuAvailabilitySearchFilter.CheckInput();
            if (checkInputResult.IsFailure)
                return checkInputResult.Error;

            var getPartnerAvailabilityResult = await _partnerAvailabilityService.Get(skuAvailabilitySearchFilter, cancellationToken);
            if (getPartnerAvailabilityResult.IsFailure)
                return ErrorBuilder.CreateRegisterNotFound(getPartnerAvailabilityResult.Error);

            return _mapper.Map<GetPartnerAvailabilityModels.Outbound>(getPartnerAvailabilityResult.Value);
        }
    }
}
