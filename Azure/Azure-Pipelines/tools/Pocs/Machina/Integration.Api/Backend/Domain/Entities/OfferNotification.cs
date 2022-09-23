using CSharpFunctionalExtensions;
using Integration.Api.Backend.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Integration.Api.Backend.Domain.Entities
{
    public class OfferNotification : Entity<string>
    {
        public NotificationStatus Status { get; private set; } = NotificationStatus.Created;

        public Offer Offer { get; private set; }

        public EnrichedOffer EnrichedOffer { get; private set; }

        public IDictionary<string, string> Metadata { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        public OfferNotification WaitingGetDetail()
        {
            if (Status.IsNot(NotificationStatus.PendingNotification))
                return this;

            Status = NotificationStatus.AwaitingGetDetail;
            LastModifiedAt = DateTime.UtcNow;

            return this;
        }

        public OfferNotification AwaitingEnrichment()
        {
            if (Status.IsNot(NotificationStatus.AwaitingGetDetail))
                return this;

            Status = NotificationStatus.AwaitingEnrichment;
            LastModifiedAt = DateTime.UtcNow;

            return this;
        }

        public OfferNotification Enriched(EnrichedOffer enrichedOffer)
        {
            if (Status.In(NotificationStatus.Created, NotificationStatus.PendingNotification))
                return this;

            if (EnrichedOffer == enrichedOffer)
                return this;

            Status = (string.IsNullOrWhiteSpace(enrichedOffer.ProductHash) && string.IsNullOrWhiteSpace(enrichedOffer.SkuHash))
                ? NotificationStatus.AwaitingCompleteEnrichment
                : NotificationStatus.Enriched;

            EnrichedOffer = enrichedOffer;
            LastModifiedAt = DateTime.UtcNow;

            return this;
        }
    }
}
