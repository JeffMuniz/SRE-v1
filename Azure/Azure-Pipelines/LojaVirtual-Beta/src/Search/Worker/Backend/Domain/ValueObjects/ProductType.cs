using CSharpFunctionalExtensions;

namespace Search.Worker.Backend.Domain.ValueObjects
{
    public class ProductType : EnumValueObject<ProductType>
    {
        public static readonly ProductType Product = new("1", "Product");
        public static readonly ProductType Service = new("2", "Service");

        public string Name { get; }

        public ProductType(string id, string name) : base(id)
        {
            Name = name;
        }

        public override string ToString() =>
            $"{Id}|{Name}";

        public int ToInteger() => 
            int.Parse(Id);
    }
}
