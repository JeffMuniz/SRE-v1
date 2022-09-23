namespace Product.Supplier.Magalu.Worker.Backend.Application.Usecases.IntegrateFullCatalog.Models
{
    public class Outbound
    {
        private static readonly Outbound Empty = new();

        public static Outbound Create() =>
            Empty;
    }
}
