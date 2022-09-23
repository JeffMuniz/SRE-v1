using System;

namespace Category.Shared.Messaging.Contracts.Category
{
    public interface CategoryUpdated
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int Id { get; }
        string Name { get; }
        DateTime UpdatedDate { get; }
        bool Active { get; }

        string User { get; }
    }
}
