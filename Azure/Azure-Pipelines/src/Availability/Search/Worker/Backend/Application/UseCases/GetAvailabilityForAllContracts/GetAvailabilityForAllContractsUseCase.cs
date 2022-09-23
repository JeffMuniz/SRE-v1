using AutoMapper;
using Availability.Search.Worker.Backend.Domain.Services;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Search.Worker.Backend.Application.UseCases.GetAvailabilityForAllContracts
{
    public class GetAvailabilityForAllContractsUseCase : IGetAvailabilityForAllContractsUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAvailabilityService _availabilityService;
        private readonly IConfigurationService _configurationService;

        public GetAvailabilityForAllContractsUseCase(
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
            var getPartnerResult = await _configurationService.GetPartner(inbound.SupplierId, cancellationToken);
            if (getPartnerResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(getPartnerResult.Error);

            var partner = getPartnerResult.Value;
            
            var availabilities = new List<Domain.Entities.Availability>();

            foreach (var contract in partner.Contracts)
            {
                var availabilityId = _mapper.Map<Domain.ValueObjects.AvailabilityId>(inbound, contract);

                var availabilityResult = await _availabilityService.GetAvailability(partner, availabilityId, cancellationToken);
                if (availabilityResult.IsFailure)
                    return _mapper.Map<SharedUsecases.Models.Error>(availabilityResult.Error);

                availabilities.Add(availabilityResult.Value);
            }

            return _mapper.Map<Models.Outbound>(availabilities);
        }
    }
}
