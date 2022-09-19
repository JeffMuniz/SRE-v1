using System;

namespace Integration.Api.BackgroundServices
{
    public sealed class CatalogOfferImportBackgroundServiceOptions
    {
        public bool Enabled { get; set; }

        public int BatchSize { get; set; }

        public int DegreeOfParallelism { get; set; }
    }
}
