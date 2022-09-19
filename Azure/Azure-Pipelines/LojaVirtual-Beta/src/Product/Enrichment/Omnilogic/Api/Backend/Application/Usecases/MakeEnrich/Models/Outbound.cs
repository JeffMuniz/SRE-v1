namespace Product.Enrichment.macnaima.Api.Backend.Application.Usecases.MakeEnrich.Models
{
    public class Outbound
    {
        private static readonly Outbound Empty = new();

        public static Outbound Create() =>
            Empty;
    }
}
