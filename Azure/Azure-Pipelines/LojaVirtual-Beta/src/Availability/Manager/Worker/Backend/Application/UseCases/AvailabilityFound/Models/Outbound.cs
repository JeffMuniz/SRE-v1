namespace Availability.Manager.Worker.Backend.Application.UseCases.AvailabilityFound.Models
{
    public class Outbound
    {
        private static readonly Outbound Empty = new();

        public static Outbound Create() =>
            Empty;
    }
}
