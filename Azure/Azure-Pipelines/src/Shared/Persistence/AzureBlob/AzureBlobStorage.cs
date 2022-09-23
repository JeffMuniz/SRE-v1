using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Persistence.AzureBlob.Configurations;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Persistence.AzureBlob
{
    public class AzureBlobStorage : IAzureBlobStorage
    {
        private readonly ILogger<AzureBlobStorage> _logger;
        private readonly IOptionsMonitor<AzureBlobStorageConfiguration> _options;

        public AzureBlobStorage(ILogger<AzureBlobStorage> logger, IOptionsMonitor<AzureBlobStorageConfiguration> options)
        {
            _logger = logger;
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<Stream> GetFileStream(string filename, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Iniciando a leitura do arquivo no Azure Blob Storage: {filename}");
            try
            {
                var container = GetNewBlobContainer();

                var client = container.GetBlobClient(blobName: filename);

                return await client.OpenReadAsync(options: new BlobOpenReadOptions(false), cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Ocorreu um erro ao tentar estabelecer uma conexão com o Blob Storage {filename}");
                throw;
            }
        }

        public Task UploadFileStream(string filename, Stream content, CancellationToken cancellationToken) =>
            UploadFile(
                filename,
                async (client, options) =>
                {
                    if (content.CanSeek)
                        content.Seek(0, SeekOrigin.Begin);

                    await client.UploadAsync(content,
                        options: options,
                        cancellationToken: cancellationToken
                    );
                },
                cancellationToken
            );

        public Task UploadFile(string path, string filename, CancellationToken cancellationToken) =>
            UploadFile(
                filename,
                (client, options) =>
                    client.UploadAsync(path,
                        options: options,
                        cancellationToken: cancellationToken
                    ),
                cancellationToken
            );

        public async Task RemoveIfExistsFileStorage(string filename, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Iniciando a remoção do arquivo do Azure Blob Storage: {filename}");
            try
            {
                var container = GetNewBlobContainer();

                var client = container.GetBlobClient(blobName: filename);

                await client.DeleteIfExistsAsync(snapshotsOption: DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);

                _logger.LogInformation($"Arquivo {filename} removido com sucesso");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Ocorreu um erro ao tentar estabelecer uma conexão com o Blob Storage {filename}");
                throw;
            }
        }

        private BlobContainerClient GetNewBlobContainer() =>
            new(
                connectionString: _options.CurrentValue.ConnectionString,
                blobContainerName: _options.CurrentValue.ContainerName
            );

        private BlobUploadOptions GetBlobUploadOptions() =>
            new()
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = _options.CurrentValue.ContentType ?? "application/octet-stream"
                },
                TransferOptions = new Azure.Storage.StorageTransferOptions
                {
                    InitialTransferSize = 1024,
                    MaximumConcurrency = Environment.ProcessorCount,
                    MaximumTransferSize = 67108864
                }
            };

        private async Task UploadFile(string filename, Func<BlobClient, BlobUploadOptions, Task> uploadAction, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Iniciando o upload do arquivo no Azure Blob Storage: {filename}");

            try
            {
                await RemoveIfExistsFileStorage(filename, cancellationToken);

                var container = GetNewBlobContainer();
                var client = container.GetBlobClient(blobName: filename);

                await client.DeleteIfExistsAsync(snapshotsOption: DeleteSnapshotsOption.None, cancellationToken: cancellationToken);

                var blobUploadOptios = GetBlobUploadOptions();
                await uploadAction(client, blobUploadOptios);

                _logger.LogInformation($"Arquivo {filename} criado com sucesso");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Ocorreu um erro ao tentar estabelecer uma conexão com o Blob Storage {filename}");
                throw;
            }
        }
    }
}
