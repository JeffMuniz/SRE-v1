using System.Collections.Generic;

namespace Shared.Messaging.Contracts.Availability.Messages.Manager
{
    public interface UnavailableSkusResponse
    {
        public int Total { get; set; }

        public bool IsLastPage { get; set; }

        public IEnumerable<Models.Manager.UnavailableSku> Skus { get; set; }
    }
}
