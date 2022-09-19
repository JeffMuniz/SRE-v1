namespace Category.Shared.Messaging.Contracts.Attribute
{
    public interface DeleteAttribute
    {
        byte HierarchyId { get; }
        string HierarchyName { get; }

        int CategoryId { get; }
        string CategoryName { get; }

        int SubcategoryId { get; }
        string SubcategoryName { get; }

        long Id { get; }

        string User { get; }
    }
}
