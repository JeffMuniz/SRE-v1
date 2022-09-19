namespace Shared.Persistence.Mongo.Configurations
{
    public interface IMongoOptions
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string CollectionPrefix { get; set; }

        string ApplicationName { get; set; }
    }
}
