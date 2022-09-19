using CSharpFunctionalExtensions;
using System;
using SharedDomain = Shared.Backend.Domain;

namespace Product.Change.Worker.Backend.Domain.Entities
{
    public class SkuIntegration : Entity<ValueObjects.SkuIntegrationId>
    {
        public SupplierSku SupplierSku { get; private set; }

        public ValueObjects.SupplierSkuHash ChangedHash { get; private set; }

        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        public DateTime LastIntegratedAt { get; private set; } = DateTime.UtcNow;

        public static SkuIntegration Create(SupplierSku supplierSku, Services.ICrcHashProviderService crcHashProviderService)
            => new()
            {
                SupplierSku = supplierSku,
                ChangedHash = ValueObjects.SupplierSkuHash.Create(supplierSku, crcHashProviderService)
            };

        public Result<SkuIntegration, ValueObjects.ErrorType> MustBeCheckChanges(TimeSpan timeToCheckChangesInExistingSkus)
        {
            var timeElapsedSinceLastIntegration = DateTime.UtcNow.Subtract(LastIntegratedAt.ToUniversalTime());
            if (timeToCheckChangesInExistingSkus > timeElapsedSinceLastIntegration)
                return ValueObjects.ErrorType.ThereIsNoChange;

            return this;
        }

        public Result<SkuIntegration, ValueObjects.ErrorType> ChangeSupplierSku(SupplierSku newSupplierSku, Services.ICrcHashProviderService crcHashProviderService)
        {
            var newChangedHash = ValueObjects.SupplierSkuHash.Create(newSupplierSku, crcHashProviderService);
            if (ChangedHash == newChangedHash)
                return ValueObjects.ErrorType.ThereIsNoChange;

            SupplierSku = newSupplierSku;
            ChangedHash = newChangedHash;
            LastModifiedAt = DateTime.UtcNow;

            return this;
        }

        public Result<SkuIntegration, ValueObjects.ErrorType> ChangePrice(SharedDomain.ValueObjects.Price newPrice)
        {
            if (SupplierSku.ChangePrice(newPrice) is var result && result.IsFailure)
                return result.Error;

            LastModifiedAt = DateTime.UtcNow;

            return this;
        }

        public Result<SkuIntegration, ValueObjects.ErrorType> ChangeActive(bool newSupplierSkuActive)
        {
            if (SupplierSku.ChangeActive(newSupplierSkuActive) is var result && result.IsFailure)
                return result.Error;

            LastModifiedAt = DateTime.UtcNow;

            return this;
        }

        public SkuIntegration UpdateLastIntegratedNow()
        {
            LastIntegratedAt = DateTime.UtcNow;

            return this;
        }

        public override string ToString() =>
            $"{Id}";
    }
}
