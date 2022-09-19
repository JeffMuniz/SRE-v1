using System;

namespace Availability.Manager.Worker.Backend.Application.UseCases.GetUnavailableSkus.Models
{
    public class Inbound
    {
        public DateTime InitialDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
