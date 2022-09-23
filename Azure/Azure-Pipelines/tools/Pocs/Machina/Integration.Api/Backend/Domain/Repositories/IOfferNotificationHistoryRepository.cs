using Integration.Api.Backend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Domain.Repositories
{
    public interface IOfferNotificationHistoryRepository
    {
        Task Add(OfferNotificationHistory offerNotificationHistory, CancellationToken cancellationToken);
    }
}
