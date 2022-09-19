using Microsoft.Extensions.DependencyInjection;

namespace Shared.Persistence.AzureBlob.Extensions
{
    public static class AzureBlobStorageExtensions
    {
        public static IServiceCollection AddAzureBlobStoragePersistence(this IServiceCollection services) =>
            services
                .AddScoped<IAzureBlobStorage, AzureBlobStorage>();
    }
}
