using CSharpFunctionalExtensions;
using System;

namespace Integration.Api.Backend.Domain.Entities
{
    public class CatalogImportSettings : Entity<string>
    {
        public int ImportedOffersCount { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public DateTime LastModifiedAt { get; private set; }

        public CatalogImportSettings AddImportedOffersCount(int importedOffersCount)
        {
            ImportedOffersCount += importedOffersCount;
            LastModifiedAt = DateTime.UtcNow;

            return this;
        }
    }
}
