using AutoMapper;
using Availability.Search.Worker.Backend.Domain.Services;
using Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability.Client;
using CSharpFunctionalExtensions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.Search.Worker.Backend.Infrastructure.ExternalServices.Availability
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IMapper _mapper;
        private readonly IPartnerHubClient _partnerHubClient;

        public AvailabilityService(
            IMapper mapper,
            IPartnerHubClient partnerHubClient
        )
        {
            _mapper = mapper;
            _partnerHubClient = partnerHubClient;
        }

        public async Task<Result<Domain.Entities.Availability, Domain.ValueObjects.ErrorType>> GetAvailability(
            Domain.ValueObjects.PartnerConfiguration partner,
            Domain.ValueObjects.AvailabilityId availabilityId,
            CancellationToken cancellationToken = default
        )
        {

            var getAvailabilityResult = await _partnerHubClient.GetAvailability(
                new Models.AvailabilityRequest
                {
                    PartnerCode = partner.PartnerCode,
                    SupplierSkuId = availabilityId.SupplierSkuId,
                    ContractId = availabilityId.ContractId
                },
                cancellationToken
            );
            
            if (getAvailabilityResult.IsFailure && getAvailabilityResult.Error.Is(HttpStatusCode.NotFound))
                return _mapper
                    .Map(new Models.AvailabilityResult(), Domain.Entities.Availability.Create(availabilityId))
                    .AssingnMainContract(partner);

            var availability = _mapper
                .Map(getAvailabilityResult.Value, Domain.Entities.Availability.Create(availabilityId))
                .AssingnMainContract(partner);

            return availability;
        }
    }
}
