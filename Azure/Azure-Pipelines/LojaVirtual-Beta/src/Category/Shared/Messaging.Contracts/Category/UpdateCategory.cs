namespace Category.Shared.Messaging.Contracts.Category
{
    public interface UpdateCategory
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int Id { get; }
        string Name { get; }
        bool Active { get; }

        string User { get; }
    }
}
