using System;
using System.Collections.Generic;

namespace Category.Shared.Messaging.Contracts.Attribute
{
    public interface AttributeUpdated
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int CategoryId { get; }
        string CategoryName { get; }

        int SubcategoryId { get; }
        string SubcategoryName { get; }

        long Id { get; }
        string Name { get; }
        bool Multivalued { get; }
        string Type { get; }
        string UnitName { get; }
        string UnitSymbol { get; }
        bool Required { get; }
        IEnumerable<AttributeValue> Values { get; }
        DateTime UpdatedDate { get; }
        bool Active { get; }

        string User { get; }
    }
}
