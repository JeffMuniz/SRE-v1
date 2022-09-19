namespace Category.Shared.Messaging.Contracts.Hierarchy
{
    public interface CreateHierarchy
    {
        string Name { get; }
        bool Active { get; }

        string User { get; }
    }
}
