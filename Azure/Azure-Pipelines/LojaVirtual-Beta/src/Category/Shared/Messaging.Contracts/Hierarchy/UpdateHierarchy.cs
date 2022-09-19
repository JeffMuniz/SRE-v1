namespace Category.Shared.Messaging.Contracts.Hierarchy
{
    public interface UpdateHierarchy
    {
        byte Id { get; }
        string Name { get; }
        bool Active { get; }

        string User { get; }
    }
}
