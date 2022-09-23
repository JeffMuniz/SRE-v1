using AutoMapper;
using MongoDB.Driver;
using Product.Categorization.Worker.Backend.Domain.Repositories;
using Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Repositories
{
    public class KnowledgeDataRepository : IKnowledgeDataRepository
    {
        private readonly IMapper _mapper;
        private readonly ICategorizationContext _categorizationContext;

        public KnowledgeDataRepository(
            IMapper mapper,
            ICategorizationContext categorizationContext
        )
        {
            _mapper = mapper;
            _categorizationContext = categorizationContext;
        }

        public async Task<IEnumerable<Domain.Entities.KnowledgeProduct>> List(CancellationToken cancellationToken)
        {
            var knowledgeProducts = await _categorizationContext.ProdutosCategorizados
                .Find(x => true)
                .ToListAsync(cancellationToken);

            return _mapper.Map<Domain.Entities.KnowledgeProduct[]>(knowledgeProducts);
        }
    }
}
