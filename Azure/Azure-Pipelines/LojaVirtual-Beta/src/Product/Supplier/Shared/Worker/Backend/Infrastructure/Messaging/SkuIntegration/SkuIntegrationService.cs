using AutoMapper;
using CSharpFunctionalExtensions;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChangeMessages = Shared.Messaging.Contracts.Product.Change.Messages;
using SharedMessages = Shared.Messaging.Contracts.Shared.Messages;

namespace Product.Supplier.Shared.Worker.Backend.Infrastructure.Messaging.SkuIntegration
{
    public class SkuIntegrationService : Domain.Services.ISkuIntegrationService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IRequestClient<ChangeMessages.SkuMustBeIntegrated> _skuMustBeIntegratedClient;

        public SkuIntegrationService(
            IMapper mapper,
            IBus bus,
            IRequestClient<ChangeMessages.SkuMustBeIntegrated> skuMustBeIntegratedClient
        )
        {
            _mapper = mapper;
            _bus = bus;
            _skuMustBeIntegratedClient = skuMustBeIntegratedClient;
        }

        public async Task<Result<bool, Domain.ValueObjects.ErrorType>> SkuMustBeIntegrated(
            Domain.ValueObjects.SkuMustBeIntegrated skuMustBeIntegrated,
            CancellationToken cancellationToken
        )
        {
            var skuMustBeIntegratedMessage = _mapper.Map<ChangeMessages.SkuMustBeIntegrated>(skuMustBeIntegrated);
            var response = await _skuMustBeIntegratedClient.GetResponse<
                    ChangeMessages.SkuMustBeIntegratedResponse,
                    SharedMessages.NotFound,
                    SharedMessages.UnexpectedError
                >(skuMustBeIntegratedMessage, cancellationToken);

            if (response.Is<ChangeMessages.SkuMustBeIntegratedResponse>(out var successResponse))
                return successResponse.Message.MustBeIntegrated;

            if (response.Is<SharedMessages.NotFound>(out var notFoundResponse))
                return _mapper.Map<Domain.ValueObjects.ErrorType>(notFoundResponse.Message);

            if (response.Is<SharedMessages.UnexpectedError>(out var unexpectedError))
                return _mapper.Map<Domain.ValueObjects.ErrorType>(unexpectedError.Message);

            throw new NotSupportedException(nameof(SkuMustBeIntegrated));
        }

        public async Task IntegrateSku(Domain.Entities.SupplierSku supplierSku, CancellationToken cancellationToken)
        {
            var integrateSkuMessage = _mapper.Map<ChangeMessages.IntegrateSku>(supplierSku);

            await _bus.Send(integrateSkuMessage, cancellationToken);
        }
    }
}
