using System;

namespace Shared.Messaging.Contracts.Availability.Messages.Manager
{
    public interface GetUnavailableSkus
    {
        public DateTime InitialDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
