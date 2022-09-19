using CSharpFunctionalExtensions;

namespace Search.Worker.Backend.Domain.ValueObjects
{
    public class SupplierType : EnumValueObject<SupplierType>
    {
        public static readonly SupplierType Ecommerce = new("1", "Ecommerce");

        public string Name { get; }

        public SupplierType(string id, string name) : base(id)
        {
            Name = name;
        }

        public override string ToString() =>
            $"{Id}|{Name}";

        public int ToInteger() => int.Parse(Id);
    }
}
