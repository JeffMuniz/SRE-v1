using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Availability.Api.Backend.Domain.ValueObjects
{
    public class SkuAvailabilityId : ValueObject
    {
        public static readonly SkuAvailabilityId Empty = new ();

        public int SupplierId { get; init; }

        public string SupplierSkuId { get; init; }

        public string ContractId { get; init; }

        public Result<
            SkuAvailabilityId,
            (
                UnitResult<ErrorType> SupplierId,
                UnitResult<ErrorType> SupplierSkuId,
                UnitResult<ErrorType> ContractId
            )> Check()
        {            
            var checkResult = (
                SupplierId: CheckSupplierId(),
                SupplierSkuId: CheckSupplierSkuId(),
                ContractId: CheckContractId()
            );

            if (checkResult.SupplierId.IsFailure || checkResult.SupplierSkuId.IsFailure || checkResult.ContractId.IsFailure)
                return checkResult;

            return this;
        }

        public Result<SkuAvailabilityId, ErrorType> CheckSupplierId()
        {
            if (SupplierId <= 0)
                return ErrorType.InvalidInput;

            return this;
        }

        public Result<SkuAvailabilityId, ErrorType> CheckSupplierSkuId()
        {
            if (string.IsNullOrWhiteSpace(SupplierSkuId))
                return ErrorType.InvalidInput;

            return this;
        }

        public Result<SkuAvailabilityId, ErrorType> CheckContractId()
        {
            if (string.IsNullOrWhiteSpace(ContractId))
                return ErrorType.InvalidInput;

            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SupplierId;
            yield return SupplierSkuId;
            yield return ContractId;
        }

        public override string ToString() =>
            $"{SupplierId}|{SupplierSkuId}|{ContractId}";
    }
}
