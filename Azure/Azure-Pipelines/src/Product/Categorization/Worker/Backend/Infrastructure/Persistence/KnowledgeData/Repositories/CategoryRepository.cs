using AutoMapper;
using CSharpFunctionalExtensions;
using MongoDB.Driver;
using Product.Categorization.Worker.Backend.Domain.Repositories;
using Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Categorization.Worker.Backend.Infrastructure.Persistence.KnowledgeData.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly ICategorizationContext _categorizationContext;

        public CategoryRepository(
            IMapper mapper,
            ICategorizationContext categorizationContext
        )
        {
            _mapper = mapper;
            _categorizationContext = categorizationContext;
        }

        public async Task<IEnumerable<Domain.Entities.Category>> List(CancellationToken cancellationToken)
        {
            var categories = await _categorizationContext.Categorias
                .Find(x => true)
                .ToListAsync(cancellationToken);

            var subCategories = await _categorizationContext.Subcategorias
                .Find(x => true)
                .ToListAsync(cancellationToken);

            var categoriesAndSubcategories = categories
                .Select(cat =>
                    KeyValuePair.Create(cat, subCategories.Where(sub => sub.IdNumericoCategoria == cat.IdNumerico))
                );

            return _mapper.Map<Domain.Entities.Category[]>(categoriesAndSubcategories);
        }
    }
}
