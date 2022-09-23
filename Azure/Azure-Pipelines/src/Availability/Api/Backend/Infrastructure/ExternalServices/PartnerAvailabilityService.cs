using AutoMapper;
using Availability.Api.Backend.Domain.Services;
using CSharpFunctionalExtensions;
using MassTransit;
using System.Threading;
using System.Threading.Tasks;
using AvailabilityMessaging = Shared.Messaging.Contracts.Availability.Messages;
using SharedMessages = Shared.Messaging.Contracts.Shared.Messages;

namespace Availability.Api.Backend.Infrastructure.ExternalServices
{
    public class PartnerAvailabilityService : IPartnerAvailabilityService
    {
        private readonly IMapper _mapper;
        private readonly IRequestClient<AvailabilityMessaging.Search.GetAvailability> _getAvailabilityClient;

        public PartnerAvailabilityService(
            IMapper mapper, 
            IRequestClient<AvailabilityMessaging.Search.GetAvailability> getAvailabilityClient
        )
        {
            _mapper = mapper;
            _getAvailabilityClient = getAvailabilityClient;
        }

        public async Task<Result<Domain.Entities.SkuAvailability>> Get(
            Domain.ValueObjects.SkuAvailabilitySearchFilter availabilitySearchFilter, 
            CancellationToken cancellationToken
        )
        {
            var getAvailability = _mapper.Map<AvailabilityMessaging.Search.GetAvailability>(availabilitySearchFilter);

            var response = await _getAvailabilityClient.GetResponse<
                    AvailabilityMessaging.Search.AvailabilityFound,
                    SharedMessages.NotFound,
                    SharedMessages.UnexpectedError
                >(getAvailability, cancellationToken);

            if (response.Is<AvailabilityMessaging.Search.AvailabilityFound>(out var successResponse))
                return _mapper.Map<Domain.Entities.SkuAvailability>(availabilitySearchFilter, successResponse.Message);

            if (response.Is<SharedMessages.NotFound>(out var notFoundResponse))
                return Result.Failure<Domain.Entities.SkuAvailability>(notFoundResponse.Message.Message);

            if (response.Is<SharedMessages.UnexpectedError>(out var unexpectedErrorResponse))
                return Result.Failure<Domain.Entities.SkuAvailability>(unexpectedErrorResponse.Message.Message);

            return Result.Failure<Domain.Entities.SkuAvailability>("The requested availability sku failed");
        }
    }
}
