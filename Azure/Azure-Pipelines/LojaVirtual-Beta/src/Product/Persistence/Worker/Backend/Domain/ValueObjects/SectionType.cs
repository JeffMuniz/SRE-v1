using CSharpFunctionalExtensions;

namespace Product.Persistence.Worker.Backend.Domain.ValueObjects
{
    public class SectionType : EnumValueObject<SectionType>
    {
        public static readonly SectionType Category = new("1", "Categoria");

        public static readonly SectionType Subcategory = new("2", "Subcategoria");

        public static readonly SectionType Brand = new("3", "Marca");

        public string Name { get; }

        public SectionType(string id, string name) : base(id)
        {
            Name = name;
        }

        public override string ToString() =>
            $"{Id}|{Name}";

        public int ToInteger() =>
            int.Parse(Id);
    }
}
