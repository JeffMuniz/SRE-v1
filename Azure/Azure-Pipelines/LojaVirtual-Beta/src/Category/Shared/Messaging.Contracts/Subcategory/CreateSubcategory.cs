namespace Category.Shared.Messaging.Contracts.Subcategory
{
    public interface CreateSubcategory
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int CategoryId { get; }
        string CategoryName { get; }

        int Id { get; }
        string Name { get; }
        bool Active { get; }

        string User { get; }
    }
}
