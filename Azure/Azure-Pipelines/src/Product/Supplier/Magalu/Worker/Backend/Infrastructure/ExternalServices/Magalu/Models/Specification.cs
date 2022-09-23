namespace Product.Supplier.Magalu.Worker.Backend.Infrastructure.ExternalServices.Magalu.Models
{
    public class Specification
    {
        public string Value { get; init; }

        public static Specification Create(string value) 
            => new() { Value = value };
    }
}
