using System;
using System.Data;

namespace Shared.Persistence.SqlServer
{
    public interface ISqlServerContext : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
