using System;

namespace Category.Shared.Messaging.Contracts.Hierarchy
{
    public interface HierarchyCreated
    {
        byte Id { get; }
        string Name { get; }
        DateTime CreatedDate { get; }
        bool Active { get; }

        string User { get; }
    }
}
