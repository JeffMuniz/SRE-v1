using System;

namespace Category.Shared.Messaging.Contracts.Category
{
    public interface CategoryCreated
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int Id { get; }
        string Name { get; }
        DateTime CreatedDate { get; }
        bool Active { get; }

        string User { get; }
    }
}
