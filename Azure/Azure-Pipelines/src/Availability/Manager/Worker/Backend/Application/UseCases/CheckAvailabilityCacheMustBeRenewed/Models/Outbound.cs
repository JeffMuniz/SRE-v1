namespace Availability.Manager.Worker.Backend.Application.UseCases.CheckAvailabilityCacheMustBeRenewed.Models
{
    public class Outbound
    {
        private static readonly Outbound Empty = new();

        public static Outbound Create() =>
            Empty;
    }
}
