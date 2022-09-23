using AutoMapper;
using CSharpFunctionalExtensions;
using Search.Worker.Backend.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Search.Worker.Backend.Application.Usecases.UpsertSku
{
    public class UpsertSkuUsecase : IUpsertSkuUsecase
    {
        private readonly IMapper _mapper;
        private readonly ISearchIndexRepository _searchIndexRepository;

        public UpsertSkuUsecase(IMapper mapper, ISearchIndexRepository searchIndexRepository)
        {
            _mapper = mapper;
            _searchIndexRepository = searchIndexRepository;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var sku = _mapper.Map<Domain.Entities.Sku>(inbound);

            var getSkuResult = await _searchIndexRepository.Get(sku.Id, cancellationToken);
            if (getSkuResult.IsFailure)
            {
                if (getSkuResult.Error.NotIs(Domain.ValueObjects.ErrorType.NotFound))
                    return _mapper.Map<SharedUsecases.Models.Error>(getSkuResult.Error);

                var newSku = sku;
                var addSkuResult = await _searchIndexRepository.Add(newSku, cancellationToken);
                if (addSkuResult.IsFailure)
                    return _mapper.Map<SharedUsecases.Models.Error>(addSkuResult.Error);

                return _mapper.Map<Models.Outbound>(newSku);
            }

            var existingSku = _mapper.Map(sku, getSkuResult.Value);
            var updateSkuResult = await _searchIndexRepository.Update(existingSku, cancellationToken);
            if (updateSkuResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(updateSkuResult.Error);

            return _mapper.Map<Models.Outbound>(existingSku);
        }
    }
}
