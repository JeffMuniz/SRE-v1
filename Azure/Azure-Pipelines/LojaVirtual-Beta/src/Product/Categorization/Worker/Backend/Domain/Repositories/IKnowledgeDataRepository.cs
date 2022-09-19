using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Categorization.Worker.Backend.Domain.Repositories
{
    public interface IKnowledgeDataRepository
    {
        Task<IEnumerable<Entities.KnowledgeProduct>> List(CancellationToken cancellationToken);
    }
}
