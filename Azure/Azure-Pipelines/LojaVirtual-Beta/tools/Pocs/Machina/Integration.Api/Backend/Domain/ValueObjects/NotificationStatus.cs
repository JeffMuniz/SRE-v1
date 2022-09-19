using CSharpFunctionalExtensions;

namespace Integration.Api.Backend.Domain.ValueObjects
{
    public class NotificationStatus : EnumValueObject<NotificationStatus>
    {
        public static readonly NotificationStatus Created = new(nameof(Created));

        public static readonly NotificationStatus PendingNotification = new(nameof(PendingNotification));

        public static readonly NotificationStatus AwaitingGetDetail = new(nameof(AwaitingGetDetail));

        public static readonly NotificationStatus AwaitingEnrichment = new(nameof(AwaitingEnrichment));

        public static readonly NotificationStatus AwaitingCompleteEnrichment = new(nameof(AwaitingCompleteEnrichment));

        public static readonly NotificationStatus Enriched = new(nameof(Enriched));

        public NotificationStatus(string id) : base(id)
        {
        }

        public override string ToString() =>
            $"{Id}";
    }
}
