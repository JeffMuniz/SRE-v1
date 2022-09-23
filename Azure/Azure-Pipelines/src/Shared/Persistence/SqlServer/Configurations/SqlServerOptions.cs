using System;

namespace Shared.Persistence.SqlServer.Configurations
{
    public class SqlServerOptions
    {
        public string ConnectionString { get; set; }

        public TimeSpan CommandTimeout { get; set; }
    }
}
