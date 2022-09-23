using Shared.Persistence.SqlServer.Configurations;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Shared.Persistence.SqlServer
{
    public abstract class SqlServerContext : ISqlServerContext
    {
        protected readonly SqlServerOptions _options;

        private readonly Lazy<IDbConnection> _connection;
        public virtual IDbConnection Connection =>
            _connection.Value;

        protected SqlServerContext(SqlServerOptions options)
        {
            _options = options;
            _connection = new Lazy<IDbConnection>(() => new SqlConnection(options.ConnectionString));
        }

        #region [Dispose]

        protected bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                Connection?.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SqlServerContext()
        {
            Dispose(false);
        }

        #endregion [Dispose]
    }
}
