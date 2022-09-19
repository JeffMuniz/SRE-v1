using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Categorization.Worker.Backend.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Entities.Category>> List(CancellationToken cancellationToken);
    }
}
