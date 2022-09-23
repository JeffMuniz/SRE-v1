using AutoMapper;
using Availability.Search.Worker.Backend.Domain.Services;
using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Search.Worker.Backend.Application.UseCases.GetAvailability
{
    public class GetAvailabilityUseCase : IGetAvailabilityUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAvailabilityService _availabilityService;
        private readonly IConfigurationService _configurationService;

        public GetAvailabilityUseCase(
            IAvailabilityService availabilityService,
            IConfigurationService configurationService,
            IMapper mapper
        )
        {
            _availabilityService = availabilityService;
            _configurationService = configurationService;
            _mapper = mapper;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var availabilityId = _mapper.Map<Domain.ValueObjects.AvailabilityId>(inbound);

            var getPartner = await _configurationService.GetPartner(inbound.SupplierId, cancellationToken);
            if (getPartner.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(getPartner.Error);

            var partner = getPartner.Value;

            var availability = await _availabilityService.GetAvailability(partner, availabilityId, cancellationToken);
            if (availability.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(availability.Error);

            return _mapper.Map<Models.Outbound>(availability.Value);
        }
    }
}
