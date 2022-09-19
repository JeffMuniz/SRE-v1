namespace Category.Shared.Messaging.Contracts.Subcategory
{
    public interface DeleteSubcategory
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int CategoryId { get; }
        string CategoryName { get; }

        int Id { get; }

        string User { get; }
    }
}
