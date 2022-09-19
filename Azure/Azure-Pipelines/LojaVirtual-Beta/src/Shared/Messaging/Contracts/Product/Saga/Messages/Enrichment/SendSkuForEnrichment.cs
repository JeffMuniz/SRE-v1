namespace Shared.Messaging.Contracts.Product.Saga.Messages.Enrichment
{
    public interface SendSkuForEnrichment
    {
        public string SkuIntegrationId { get; set; }

        public int SupplierId { get; set; }

        public string SkuId { get; set; }
    }
}
