namespace Category.Shared.Messaging.Contracts.Category
{
    public interface DeleteCategory
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int Id { get; }

        string User { get; }
    }
}
