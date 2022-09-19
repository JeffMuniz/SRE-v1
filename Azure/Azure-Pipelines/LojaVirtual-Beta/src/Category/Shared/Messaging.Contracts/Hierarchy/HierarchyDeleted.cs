using System;

namespace Category.Shared.Messaging.Contracts.Hierarchy
{
    public interface HierarchyDeleted
    {
        byte Id { get; }
        DateTime DeletedDate { get; }

        string User { get; }
    }
}
