using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Availability.Search.Worker.Backend.Application.UseCases.GetAvailabilityForAllContracts.Models
{
    public class Outbound : ReadOnlyCollection<Shared.Models.AvailabilityFound>
    {
        public Outbound(IEnumerable<Shared.Models.AvailabilityFound> availabilities)
            : base(availabilities.AsList())
        {
        }
    }
}
