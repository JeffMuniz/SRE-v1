using Integration.Api.Backend.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Domain.Repositories
{
    public interface IOfferNotificationRepository
    {
        Task<IEnumerable<OfferNotification>> GetNextPendings(int next, CancellationToken cancellationToken);

        Task<IEnumerable<OfferNotification>> GetNextBatchImport(int offset, int next, CancellationToken cancellationToken);

        Task<OfferNotification> Get(string id, CancellationToken cancellationToken);

        Task Add(OfferNotification offerNotification, CancellationToken cancellationToken);

        Task BulkAdd(IEnumerable<OfferNotification> offersNotification, CancellationToken cancellationToken);

        Task Update(OfferNotification offerNotification, CancellationToken cancellationToken);
    }
}
