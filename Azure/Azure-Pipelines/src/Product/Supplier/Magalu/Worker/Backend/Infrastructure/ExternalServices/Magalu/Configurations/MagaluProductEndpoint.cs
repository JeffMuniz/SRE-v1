using System;

namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Configurations
{
    public class MagaluProductEndpoint
    {
        public Uri Url { get; set; }

        public string Authorization { get; set; }

        public int PageSize { get; set; }

        public int RetryCount { get; set; }
    }
}
