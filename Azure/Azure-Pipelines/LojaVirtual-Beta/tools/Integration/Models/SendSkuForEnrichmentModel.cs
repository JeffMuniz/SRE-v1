using SagaMessages = Shared.Messaging.Contracts.Product.Saga.Messages;

namespace Tools.Integration.Models
{
    public class SendSkuForEnrichmentModel : SagaMessages.Enrichment.SendSkuForEnrichment
    {
        public string SkuIntegrationId { get; set; }

        public int SupplierId { get; set; }

        public string SkuId { get; set; }
    }
}
