using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Integration.Api.Backend.Infrastructure.Persistence.Databases.Catalog
{
    public class CatalogDatabase : ICatalogDatabase
    {
        public IDbConnection Connection { get; }

        public CatalogDatabase(IOptions<CatalogDatabaseSettings> settings)
        {
            Connection = new SqlConnection(settings.Value.ConnectionString);
        }

        #region [Dispose]

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Connection.Dispose();
            }

            _disposed = true;
        }

        ~CatalogDatabase()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }

        #endregion [Dispose]
    }
}
