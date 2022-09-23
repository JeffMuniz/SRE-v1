using System.Collections.Generic;

namespace Availability.Manager.Worker.Backend.Application.UseCases.GetUnavailableSkus.Models
{
    public class Outbound
    {
        public int Total { get; set; }

        public bool IsLastPage { get; set; }

        public IEnumerable<UnavailableSku> Skus { get; set; }
    }
}
