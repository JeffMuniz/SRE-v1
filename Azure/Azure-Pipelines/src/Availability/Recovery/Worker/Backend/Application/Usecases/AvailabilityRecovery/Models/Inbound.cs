using System;

namespace Availability.Recovery.Worker.Backend.Application.Usecases.AvailabilityRecovery.Models
{
    public class Inbound
    {
        public int PageSize { get; set; }

        public DateTime InitialDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
