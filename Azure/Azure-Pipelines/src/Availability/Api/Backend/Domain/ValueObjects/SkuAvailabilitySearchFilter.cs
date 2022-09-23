using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Availability.Api.Backend.Domain.ValueObjects
{
    public class SkuAvailabilitySearchFilter : ValueObject
    {
        public SkuAvailabilityId SkuAvailabilityId { get; init; }

        public string PersistedSkuId { get; init; }

        public string ShardId { get; init; }

        public Result<
            SkuAvailabilitySearchFilter,
            (
                UnitResult<(
                    UnitResult<ErrorType> SupplierId,
                    UnitResult<ErrorType> SupplierSkuId,
                    UnitResult<ErrorType> ContractId
                )> SkuAvailabilityId,
                UnitResult<ErrorType> PersistedSkuId,
                UnitResult<ErrorType> ShardId
            )> Check()
        {
            var checkResult = (
                SkuAvailabilityId: CheckSkuAvailabilityId(),
                PersistedSkuId: CheckPersistedSkuId(),
                ShardId: CheckShardId()
            );

            if (checkResult.SkuAvailabilityId.IsFailure || checkResult.PersistedSkuId.IsFailure || checkResult.ShardId.IsFailure)
                return checkResult;

            return this;
        }

        public Result<
            SkuAvailabilitySearchFilter,
            (
                UnitResult<ErrorType> SupplierId,
                UnitResult<ErrorType> SupplierSkuId,
                UnitResult<ErrorType> ContractId
            )> CheckSkuAvailabilityId()
        {
            if ((SkuAvailabilityId ?? SkuAvailabilityId.Empty).Check() is var check && check.IsFailure)
                return check.Error;

            return this;
        }

        public Result<SkuAvailabilitySearchFilter, ErrorType> CheckPersistedSkuId()
        {
            if (string.IsNullOrWhiteSpace(PersistedSkuId))
                return ErrorType.InvalidInput;

            return this;
        }

        public Result<SkuAvailabilitySearchFilter, ErrorType> CheckShardId()
        {
            if (string.IsNullOrWhiteSpace(ShardId))
                return ErrorType.InvalidInput;

            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SkuAvailabilityId;
            yield return PersistedSkuId;
            yield return ShardId;
        }

        public override string ToString() =>
            $"{SkuAvailabilityId}|{PersistedSkuId}|{ShardId}";
    }
}
