using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Product.Categorization.Worker.Backend.Domain.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SharedUsecases = Shared.Backend.Application.Usecases;

namespace Product.Categorization.Worker.Backend.Application.Usecases.CategorizeSku
{
    public class CategorizeSkuUsecase : ICategorizeSkuUsecase
    {
        private readonly IMapper _mapper;
        private readonly IOptionsMonitor<Options.CategorizeOptions> _categorizeSkuOptions;
        private readonly ICategorizerService _categorizerService;

        public CategorizeSkuUsecase(
            IMapper mapper,
            IOptionsMonitor<Options.CategorizeOptions> categorizeSkuOptions,
            ICategorizerService categorizerService
        )
        {
            _mapper = mapper;
            _categorizeSkuOptions = categorizeSkuOptions;
            _categorizerService = categorizerService;
        }

        public async Task<Result<Models.Outbound, SharedUsecases.Models.Error>> Execute(Models.Inbound inbound, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Domain.Entities.Product>(inbound);

            var categorizerComputeResult = await _categorizerService.Compute(product, cancellationToken);
            if (categorizerComputeResult.IsFailure)
                return _mapper.Map<SharedUsecases.Models.Error>(categorizerComputeResult.Error);

            var topSubcategoriesProbabilities = categorizerComputeResult.Value
                .OrderByDescending(x => x.Probability)
                .Take(_categorizeSkuOptions.CurrentValue.TopProbabilities);

            var approvalThreshold = _categorizeSkuOptions.CurrentValue.ApprovalThreshold;
            var categorizationResult = Domain.ValueObjects.CategorizationResult.Create(approvalThreshold, topSubcategoriesProbabilities);

            return _mapper.Map<Models.Outbound>(categorizationResult);
        }
    }
}
