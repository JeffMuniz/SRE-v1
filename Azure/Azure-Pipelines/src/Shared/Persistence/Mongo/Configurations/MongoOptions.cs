namespace Shared.Persistence.Mongo.Configurations
{
    public class MongoOptions : IMongoOptions
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionPrefix { get; set; }

        public string ApplicationName { get; set; }
    }
}
