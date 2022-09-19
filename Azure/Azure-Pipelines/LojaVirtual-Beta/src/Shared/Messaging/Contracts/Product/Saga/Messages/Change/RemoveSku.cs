namespace Shared.Messaging.Contracts.Product.Saga.Messages.Change
{
    public interface RemoveSku
    {
        public string SkuIntegrationId { get; set; }

        int SupplierId { get; set; }

        string SupplierSkuId { get; set; }
    }
}
