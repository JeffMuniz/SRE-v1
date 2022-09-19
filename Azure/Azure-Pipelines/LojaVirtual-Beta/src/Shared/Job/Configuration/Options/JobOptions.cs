namespace Shared.Job.Configuration.Options
{
    public class JobOptions
    {
        public string StoragePrefix { get; set; }

        public string StorageConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
