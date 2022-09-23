using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Availability.Manager.Worker.Backend.Domain.ValueObjects
{
    public class CacheRenewScheduleId : ValueObject
    {
        public int SupplierId { get; init; }

        public string SupplierSkuId { get; init; }

        public string ContractId { get; init; }

        public ShardId ShardId { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SupplierId;
            yield return SupplierSkuId;
            yield return ContractId;
            yield return ShardId;
        }

        public override string ToString() =>
            $"{SupplierId}|{SupplierSkuId}|{ContractId}|{ShardId}";
    }
}
