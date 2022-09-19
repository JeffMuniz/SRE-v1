using System;

namespace Category.Shared.Messaging.Contracts.Hierarchy
{
    public interface HierarchyUpdated
    {
        byte Id { get; }
        string Name { get; }
        DateTime UpdatedDate { get; }
        bool Active { get; }

        string User { get; }
    }
}
