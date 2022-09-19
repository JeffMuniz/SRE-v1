using Automatonymous;
using MassTransit.Saga;
using MongoDB.Bson.Serialization.Attributes;
using System;
using MessagingContracts = Shared.Messaging.Contracts;

namespace Product.Saga.Worker.Saga.States
{
    public class SkuState : SagaStateMachineInstance, ISagaVersion
    {
        public Models.SkuFlowType FlowType { get; set; }

        public Guid CorrelationId { get; set; }

        public string SkuIntegrationId { get; set; }

        public MessagingContracts.Shared.Models.SupplierSku SupplierSku { get; set; }

        public MessagingContracts.Shared.Models.CategorizedData CategorizedData { get; set; }

        public MessagingContracts.Shared.Models.EnrichedData EnrichedData { get; set; }

        public Models.PersistedSku PersistedSku { get; set; }

        public string CurrentState { get; set; }

        [BsonIgnoreIfDefault(true)]
        public int RemoveCompletedStatus { get; set; }

        public int Version { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
