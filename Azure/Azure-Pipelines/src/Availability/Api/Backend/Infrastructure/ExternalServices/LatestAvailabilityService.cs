using AutoMapper;
using Availability.Api.Backend.Domain.Services;
using CSharpFunctionalExtensions;
using MassTransit;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using System.Threading;
using System.Threading.Tasks;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;
using SharedMessages = Shared.Messaging.Contracts.Shared.Messages;

namespace Availability.Api.Backend.Infrastructure.ExternalServices
{
    public class LatestAvailabilityService : ILatestAvailabilityService
    {
        private readonly IMapper _mapper;
        private readonly IRequestClient<GetLatestAvailability> _getLatestAvailabilityClient;

        public LatestAvailabilityService(
            IMapper mapper, 
            IRequestClient<GetLatestAvailability> getLatestAvailabilityClient
        )
        {
            _mapper = mapper;
            _getLatestAvailabilityClient = getLatestAvailabilityClient;
        }

        public async Task<Result<Domain.Entities.SkuAvailability>> Get(
            Domain.ValueObjects.SkuAvailabilitySearchFilter latestAvailabilitySearchFilter, 
            CancellationToken cancellationToken
        )
        {
            var getLatestAvailability = _mapper.Map<GetLatestAvailability>(latestAvailabilitySearchFilter);

            var response = await _getLatestAvailabilityClient.GetResponse<
                    GetLatestAvailabilityResponse,
                    SharedMessages.NotFound,
                    SharedMessages.UnexpectedError
                >(getLatestAvailability, cancellationToken);

            if (response.Is<GetLatestAvailabilityResponse>(out var successResponse))
                return _mapper.Map<Domain.Entities.SkuAvailability>(latestAvailabilitySearchFilter, successResponse.Message);

            if (response.Is<SharedMessages.NotFound>(out var notFoundResponse))
                return Result.Failure<Domain.Entities.SkuAvailability>(notFoundResponse.Message.Message);

            if (response.Is<SharedMessages.UnexpectedError>(out var unexpectedErrorResponse))
                return Result.Failure<Domain.Entities.SkuAvailability>(unexpectedErrorResponse.Message.Message);

            return Result.Failure<Domain.Entities.SkuAvailability>("The requested latest sku failed");
        }
    }
}
