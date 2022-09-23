using System;

namespace Category.Shared.Messaging.Contracts.Category
{
    public interface CategoryDeleted
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int Id { get; }
        DateTime DeletedDate { get; }

        string User { get; }
    }
}
