using CSharpFunctionalExtensions;

namespace Product.Persistence.Worker.Backend.Domain.Entities
{
    public class Section : Entity<string>
    {
        public ValueObjects.SectionType SectionType { get; init; }

        public string Value { get; init; }

        public Section Parent { get; init; }

        public override string ToString() => $"{Id}";

        public static Section CreateBrand(string id, string value)
            => Create(id, value, ValueObjects.SectionType.Brand);

        public static Section CreateCategory(string id, string value)
            => Create(id, value, ValueObjects.SectionType.Category);

        public static Section CreateSubcategory(Section parent, string id, string value)
            => Create(parent, id, value, ValueObjects.SectionType.Subcategory);

        public static Section Create(string id, string value, ValueObjects.SectionType sectionType)
            => new() { Id = id, SectionType = sectionType, Value = value };

        public static Section Create(Section parent, string id, string value, ValueObjects.SectionType sectionType)
            => new() { Id = id, Parent = parent, SectionType = sectionType, Value = value };
    }
}
