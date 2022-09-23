namespace Product.Change.Worker.Backend.Application.Usecases.IntegrateSku.Models
{
    public class Outbound
    {
        private static readonly Outbound Empty = new();

        public static Outbound Create() =>
            Empty;
    }
}
