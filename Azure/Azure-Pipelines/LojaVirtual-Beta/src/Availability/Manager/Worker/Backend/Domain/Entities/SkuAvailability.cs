using CSharpFunctionalExtensions;
using Shared.Backend.Domain.ValueObjects;
using System;

namespace Availability.Manager.Worker.Backend.Domain.Entities
{
    public class SkuAvailability : Entity<ValueObjects.SkuAvailabilityId>
    {
        public string PersistedSkuId { get; private set; }

        public bool MainContract { get; private set; }

        public bool Available { get; private set; }

        public Price Price { get; private set; }

        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

        public DateTime LatestUpdatedDate { get; private set; } = DateTime.UtcNow;

        public DateTime LatestPartnerAvailabilityFoundDate { get; private set; } = DateTime.UtcNow;

        public Result<
            (
                UnitResult<ValueObjects.ErrorType> PersistedSkuId,
                UnitResult<ValueObjects.ErrorType> MainContract,
                UnitResult<ValueObjects.ErrorType> Available,
                UnitResult<ValueObjects.ErrorType> Price
            ), ValueObjects.ErrorType> ChangeAll(SkuAvailability newSkuAvailability)
        {
            var changeAllResult = (
                PersistedSkuId: ChangePersistedSkuId(newSkuAvailability.PersistedSkuId),
                MainContract: ChangeMainContract(newSkuAvailability.MainContract),
                Available: ChangeAvailable(newSkuAvailability.Available),
                Price: ChangePrice(newSkuAvailability.Price)
            );

            if (
                changeAllResult.PersistedSkuId.IsFailure && changeAllResult.MainContract.IsFailure &&
                changeAllResult.Available.IsFailure && changeAllResult.Price.IsFailure
            )
                return ValueObjects.ErrorType.ThereIsNoChange;

            return changeAllResult;
        }

        public Result<SkuAvailability, ValueObjects.ErrorType> ChangePersistedSkuId(string newPersistedSkuId)
        {
            if (PersistedSkuId == newPersistedSkuId)
                return ValueObjects.ErrorType.ThereIsNoChange;

            PersistedSkuId = newPersistedSkuId;
            LatestUpdatedDate = DateTime.UtcNow;

            return this;
        }

        public Result<SkuAvailability, ValueObjects.ErrorType> ChangeMainContract(bool newMainContract)
        {
            if (MainContract == newMainContract)
                return ValueObjects.ErrorType.ThereIsNoChange;

            MainContract = newMainContract;
            LatestUpdatedDate = DateTime.UtcNow;

            return this;
        }

        public Result<SkuAvailability, ValueObjects.ErrorType> ChangeAvailable(bool newAvailable)
        {
            if (Available == newAvailable)
                return ValueObjects.ErrorType.ThereIsNoChange;

            Available = newAvailable;
            LatestUpdatedDate = DateTime.UtcNow;

            return this;
        }

        public Result<SkuAvailability, ValueObjects.ErrorType> ChangePrice(Price newPrice)
        {
            if (Price == newPrice)
                return ValueObjects.ErrorType.ThereIsNoChange;

            Price = newPrice;
            LatestUpdatedDate = DateTime.UtcNow;

            return this;
        }

        public Result<SkuAvailability, ValueObjects.ErrorType> ChangeLatestPartnerAvailabilityFoundDate(DateTime newLatestPartnerAvailabilityFoundDate)
        {
            if (LatestPartnerAvailabilityFoundDate == newLatestPartnerAvailabilityFoundDate)
                return ValueObjects.ErrorType.ThereIsNoChange;

            LatestPartnerAvailabilityFoundDate = newLatestPartnerAvailabilityFoundDate;

            return this;
        }

        public bool MustBeGetPartnerAvailability(TimeSpan minTimeToGetPartnerAvailability)
        {
            var timeElapsedSinceLatestPartnerAvailabilityFound = DateTime.UtcNow.Subtract(LatestPartnerAvailabilityFoundDate.ToUniversalTime());
            return (minTimeToGetPartnerAvailability < timeElapsedSinceLatestPartnerAvailabilityFound);
        }

        public override string ToString() =>
            $"{Id}";
    }
}
