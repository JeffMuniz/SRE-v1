namespace Availability.Api.Backend.Application.UseCases.Shared.Models
{
    public abstract class Inbound
    {
        public int SupplierId { get; init; }

        public string SupplierSkuId { get; init; }

        public string ContractId { get; init; }

        public string PersistedSkuId { get; init; }

        public string ShardId { get; init; }
    }
}
