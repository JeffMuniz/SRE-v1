using CSharpFunctionalExtensions;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class SkuStatus : EnumValueObject<SkuStatus>
    {
        public static readonly SkuStatus Available = new("1", "Disponivel");

        public static readonly SkuStatus Unavailable = new("2", "Indisponivel");

        public string Name { get; }

        public SkuStatus(string id, string name) : base(id)
        {
            Name = name;
        }

        public override string ToString() =>
            $"{Id}|{Name}";

        public int ToInteger() => int.Parse(Id);
    }
}
