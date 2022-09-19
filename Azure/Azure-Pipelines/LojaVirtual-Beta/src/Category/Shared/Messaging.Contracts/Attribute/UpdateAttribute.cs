using System.Collections.Generic;

namespace Category.Shared.Messaging.Contracts.Attribute
{
    public interface UpdateAttribute
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int CategoryId { get; }
        string CategoryName { get; }

        int SubategoryId { get; }
        string SubcategoryName { get; }

        long Id { get; }
        string Name { get; }
        bool Multivalued { get; }
        string Type { get; }
        string UnitName { get; }
        string UnitSymbol { get; }
        bool Required { get; }
        IEnumerable<AttributeValue> Values { get; }
        bool Active { get; }

        string User { get; }
    }
}
