using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Memory;
using Product.Categorization.Worker.Backend.Domain.Repositories;
using Product.Categorization.Worker.Backend.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Categorization.Worker.Backend.Infrastructure.Categorizer
{
    public class CategorizerService : ICategorizerService
    {
        private static readonly SemaphoreSlim _trainedSubcategoriesSemaphore = new SemaphoreSlim(1);

        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IKnowledgeDataRepository _knowledgeDataRepository;
        private readonly ITratamentoDados _tratamentoDados;

        public CategorizerService(
            IMapper mapper,
            IMemoryCache memoryCache,
            ICategoryRepository categoryRepository,
            IKnowledgeDataRepository knowledgeDataRepository,
            ITratamentoDados tratamentoDados
        )
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _categoryRepository = categoryRepository;
            _knowledgeDataRepository = knowledgeDataRepository;
            _tratamentoDados = tratamentoDados;
        }

        public async Task<Result<IEnumerable<Domain.ValueObjects.SubcategoryProbability>, Domain.ValueObjects.ErrorType>> Compute(
            Domain.Entities.Product product,
            CancellationToken cancellationToken
        )
        {
            var trainedSubcategories = await GetTrainedSubcategoriesWithCacheControl(cancellationToken);

            var productToCategorize = _mapper.Map<Models.Produto>(product);
            var processedProduct = _tratamentoDados.ProcessarProduto(productToCategorize);

            var probabilitiesCalculator = new CalculadoraProbabilidades(trainedSubcategories);
            var probabilities = probabilitiesCalculator.CalcularVetorDeProbabilidades(processedProduct);

            return _mapper.Map<Domain.ValueObjects.SubcategoryProbability[]>(probabilities);
        }

        public async Task<IEnumerable<Models.Subcategoria>> GetTrainedSubcategoriesWithCacheControl(
            CancellationToken cancellationToken
        )
        {
            const string KEY_TRAINED_SUBCATEGORIES = "TRAINED_SUBCATEGORIES";

            if (_memoryCache.TryGetValue(KEY_TRAINED_SUBCATEGORIES, out IEnumerable<Models.Subcategoria> trainedSubcategories))
                return trainedSubcategories;

            try
            {
                await _trainedSubcategoriesSemaphore.WaitAsync(cancellationToken);

                if (_memoryCache.TryGetValue(KEY_TRAINED_SUBCATEGORIES, out trainedSubcategories))
                    return trainedSubcategories;

                var categories = _mapper.Map<IEnumerable<Models.Categoria>>(
                    await _categoryRepository.List(cancellationToken));
                var knowledgeProducts = _mapper.Map<IEnumerable<Models.ProdutoCategorizadoManualmente>>(
                    await _knowledgeDataRepository.List(cancellationToken));

                trainedSubcategories = _tratamentoDados.ProcessarESepararProdutosCategorizados(knowledgeProducts, categories);

                _memoryCache.GetOrCreate(KEY_TRAINED_SUBCATEGORIES,
                    entry => entry
                        .SetValue(trainedSubcategories)
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                        .Value
                );

            }
            finally
            {
                _trainedSubcategoriesSemaphore.Release();
            }

            return trainedSubcategories;
        }
    }
}
