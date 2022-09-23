namespace Category.Shared.Messaging.Contracts.Category
{
    public interface CreateCategory
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        string Name { get; }
        bool Active { get; }

        string User { get; }
    }
}
