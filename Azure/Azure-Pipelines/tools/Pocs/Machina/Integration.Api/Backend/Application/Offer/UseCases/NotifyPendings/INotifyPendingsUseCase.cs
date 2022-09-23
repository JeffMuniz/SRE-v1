using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.NotifyPendings
{
    public interface INotifyPendingsUseCase
    {
        Task Execute(int degreeOfParallelism, CancellationToken cancellationToken);
    }

}
