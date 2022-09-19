using AutoMapper;
using Availability.Recovery.Worker.Backend.Domain.Entities;
using Availability.Recovery.Worker.Backend.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using MassTransit;
using Shared.Messaging.Contracts.Availability.Messages.Manager;
using System.Threading;
using System.Threading.Tasks;
using AvailabilityMessages = Shared.Messaging.Contracts.Availability.Messages;
using SharedMessages = Shared.Messaging.Contracts.Shared.Messages;

namespace Availability.Recovery.Worker.Backend.Infrastructure.ExternalServices.Availability
{
    public class AvailabilityRecoveryService : Domain.Services.IAvailabilityRecoveryService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IRequestClient<GetUnavailableSkus> _getUnavailableSkusClient;

        public AvailabilityRecoveryService(
            IMapper mapper,
            IBus bus,
            IRequestClient<GetUnavailableSkus> getUnavailableSkusClient
        )
        {
            _mapper = mapper;
            _bus = bus;
            _getUnavailableSkusClient = getUnavailableSkusClient;
        }

        public async Task<Result<PagedRecoverySkus>> GetSkusForRecovery(SearchFilter request, CancellationToken cancellationToken)
        {
            var getUnavailableSkus = _mapper.Map<GetUnavailableSkus>(request);

            var response = await _getUnavailableSkusClient.GetResponse<
                    UnavailableSkusResponse,
                    SharedMessages.UnexpectedError
                >(getUnavailableSkus, cancellationToken);

            if (response.Is<UnavailableSkusResponse>(out var successResponse))
                return _mapper.Map<PagedRecoverySkus>(successResponse.Message);

            if (response.Is<SharedMessages.UnexpectedError>(out var unexpectedErrorResponse))
                return Result.Failure<PagedRecoverySkus>(unexpectedErrorResponse.Message.Message);

            return Result.Failure<PagedRecoverySkus>("The requested unavailable skus failed");
        }

        public async Task SendToGetAvailability(SkuRecovery activeAndUnvailableSku, CancellationToken cancellationToken)
        {
            var getAvailability = _mapper.Map<AvailabilityMessages.Search.GetAvailability>(activeAndUnvailableSku);

            await _bus.Send(getAvailability, cancellationToken);
        }
    }
}
