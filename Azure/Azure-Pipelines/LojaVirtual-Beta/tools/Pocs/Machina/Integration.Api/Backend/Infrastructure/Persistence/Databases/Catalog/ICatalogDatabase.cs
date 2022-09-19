using System;
using System.Data;

namespace Integration.Api.Backend.Infrastructure.Persistence.Databases.Catalog
{
    public interface ICatalogDatabase : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
