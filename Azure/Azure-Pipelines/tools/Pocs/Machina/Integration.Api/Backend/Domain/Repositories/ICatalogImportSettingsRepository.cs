using Integration.Api.Backend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Integration.Api.Backend.Domain.Repositories
{
    public interface ICatalogImportSettingsRepository
    {
        Task<CatalogImportSettings> Get(CancellationToken cancellationToken);

        Task Save(CatalogImportSettings catalogImportSettings, CancellationToken cancellationToken);
    }
}
