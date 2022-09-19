using AutoMapper;
using Availability.Recovery.Worker.Backend.Domain.Services;
using CSharpFunctionalExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Availability.Recovery.Worker.Backend.Application.Usecases.AvailabilityRecovery
{
    public class AvailabilityRecoveryUseCase : IAvailabilityRecoveryUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAvailabilityRecoveryService _availabilityRecoveryService;

        public AvailabilityRecoveryUseCase(
            IMapper mapper,
            IAvailabilityRecoveryService availabilityRecoveryService
        )
        {
            _mapper = mapper;
            _availabilityRecoveryService = availabilityRecoveryService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var searchFilter = _mapper.Map<Domain.ValueObjects.SearchFilter>(inbound);

            Domain.ValueObjects.PagedRecoverySkus pagedRecoverySkus;
            do
            {
                var getSkusForRecovery = await _availabilityRecoveryService.GetSkusForRecovery(searchFilter, cancellationToken);
                if (getSkusForRecovery.IsFailure)
                    return new SharedUsecases.Models.Error
                    {
                        Code = nameof(IAvailabilityRecoveryService.GetSkusForRecovery),
                        Message = getSkusForRecovery.Error
                    };

                pagedRecoverySkus = getSkusForRecovery.Value;

                await pagedRecoverySkus.Skus.ParallelForEachAsync(
                    maxDegreeOfParallelism: 4,
                    async skuRecovery => await _availabilityRecoveryService.SendToGetAvailability(skuRecovery, cancellationToken),
                    cancellationToken
                );

                searchFilter = Domain.ValueObjects.SearchFilter.CreateIncremented(searchFilter);
            } while (!pagedRecoverySkus.IsLastPage);

            return Models.Outbound.Create();
        }
    }
}
