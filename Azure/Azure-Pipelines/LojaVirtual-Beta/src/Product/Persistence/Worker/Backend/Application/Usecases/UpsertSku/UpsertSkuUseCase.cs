using AutoMapper;
using CSharpFunctionalExtensions;
using Product.Persistence.Worker.Backend.Domain.Services;
using Shared.Keywords;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Persistence.Worker.Backend.Application.Usecases.UpsertSku
{
    public class UpsertSkuUseCase : IUpsertSkuUseCase
    {
        private readonly IMapper _mapper;
        private readonly IKeywordsGenerator _keywordsGenerator;
        private readonly IProductStorageService _storageService;

        public UpsertSkuUseCase(
            IMapper mapper,
            IKeywordsGenerator keywordsGenerator,
            IProductStorageService storageService
        )
        {
            _mapper = mapper;
            _keywordsGenerator = keywordsGenerator;
            _storageService = storageService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Domain.Entities.Product>(inbound);

            var keywords = await _keywordsGenerator.Generate(product.Name);

            product
                .AssignKeywords(keywords);

            var persistedData = await _storageService.Store(product, cancellationToken);

            return _mapper.Map<Models.Outbound>(product, persistedData);
        }
    }
}
