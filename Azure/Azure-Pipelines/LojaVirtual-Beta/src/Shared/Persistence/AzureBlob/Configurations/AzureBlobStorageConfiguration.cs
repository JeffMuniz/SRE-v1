namespace Shared.Persistence.AzureBlob.Configurations
{
    public class AzureBlobStorageConfiguration
    {
        public string ConnectionString { get; set; }

        public string ContainerName { get; set; }

        public string ContentType { get; set; }
    }
}
