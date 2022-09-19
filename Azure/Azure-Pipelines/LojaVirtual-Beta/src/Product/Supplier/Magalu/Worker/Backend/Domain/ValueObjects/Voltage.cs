using CSharpFunctionalExtensions;

namespace Product.Supplier.Magalu.Worker.Backend.Domain.ValueObjects
{
    public class Voltage : EnumValueObject<Voltage>
    {
        public static readonly Voltage Bivolt = new("0", "Bivolt");

        public static readonly Voltage Voltage110 = new("1", "110 Volts");

        public static readonly Voltage Voltage220 = new("2", "220 Volts");

        public static readonly Voltage Voltage380 = new("4", "380 Volts");

        public static readonly Voltage None = new("3", "None");

        public string Description { get; }

        public Voltage(string id, string description) : base(id)
        {
            Description = description;
        }

        public override string ToString() =>
            $"{Id}|{Description}";
    }
}
