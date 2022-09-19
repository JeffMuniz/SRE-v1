using Shared.Messaging.Contracts.Availability.Messages.Manager;
using System;

namespace Tools.Integration.Models.Availability
{
    public class GetUnavailableSkusModel : GetUnavailableSkus
    {
        public DateTime InitialDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
