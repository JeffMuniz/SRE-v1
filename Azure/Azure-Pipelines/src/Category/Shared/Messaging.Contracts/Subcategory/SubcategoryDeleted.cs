using System;

namespace Category.Shared.Messaging.Contracts.Subcategory
{
    public interface SubcategoryDeleted
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int CategoryId { get; }
        string CategoryName { get; }

        int Id { get; }
        DateTime DeletedDate { get; }

        string User { get; }
    }
}
