namespace Product.Change.Worker.Backend.Application.Usecases.SkuMustBeIntegrated.Models
{
    public class Outbound
    {
        public bool MustBeIntegrated { get; set; }

        public static Outbound CreateMustIntegrated() =>
            new() { MustBeIntegrated = true };

        public static Outbound CreateNotMustIntegrated() =>
            new() { MustBeIntegrated = false };

    }
}
