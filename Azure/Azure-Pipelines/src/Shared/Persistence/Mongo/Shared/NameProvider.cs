namespace Shared.Persistence.Mongo.Shared
{
    internal static class NameProvider
    {
        public static string GetContextName<TContext>()
            where TContext : class, IMongoContext =>
            typeof(TContext).FullName;
    }
}
