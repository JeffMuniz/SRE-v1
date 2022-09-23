using System;

namespace Category.Shared.Messaging.Contracts.Attribute
{
    public interface AttributeDeleted
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int CategoryId { get; }
        string CategoryName { get; }

        int SubcategoryId { get; }
        string SubcategoryName { get; }

        long Id { get; }
        DateTime DeletedDate { get; }

        string User { get; }
    }
}
