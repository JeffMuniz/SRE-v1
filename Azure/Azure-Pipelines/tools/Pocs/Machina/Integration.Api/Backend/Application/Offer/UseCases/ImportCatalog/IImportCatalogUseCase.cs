using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Application.Offer.UseCases.ImportCatalog
{
    public interface IImportCatalogUseCase
    {
        Task Execute(int batchSize, int degreeOfParallelism, CancellationToken cancellationToken);
    }

}
