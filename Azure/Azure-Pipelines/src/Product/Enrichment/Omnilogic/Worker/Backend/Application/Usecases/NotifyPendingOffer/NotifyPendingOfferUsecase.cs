using AutoMapper;
using CSharpFunctionalExtensions;
using Product.Enrichment.Macnaima.Worker.Backend.Domain.Services;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Enrichment.Macnaima.Worker.Backend.Application.Usecases.NotifyPendingOffer
{
    public class NotifyPendingOfferUsecase : INotifyPendingOfferUsecase
    {
        private readonly IMapper _mapper;
        private readonly IMacnaimaService _macnaimaService;

        public NotifyPendingOfferUsecase(
            IMapper mapper,
            IMacnaimaService macnaimaService
        )
        {
            _mapper = mapper;
            _macnaimaService = macnaimaService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var offerId = _mapper.Map<Domain.ValueObjects.OfferId>(inbound);
            var notifyOfferResult = await _macnaimaService.NotifyOffer(offerId, cancellationToken);
            if (notifyOfferResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(notifyOfferResult);

            return Models.Outbound.Create();
        }
    }
}
