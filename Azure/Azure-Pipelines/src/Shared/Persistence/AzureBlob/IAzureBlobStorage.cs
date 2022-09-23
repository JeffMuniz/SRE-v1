using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Persistence.AzureBlob
{
    public interface IAzureBlobStorage
    {
        Task<Stream> GetFileStream(string filename, CancellationToken cancellationToken);

        Task UploadFileStream(string filename, Stream content, CancellationToken cancellationToken);

        Task RemoveIfExistsFileStorage(string filename, CancellationToken cancellationToken);
    }
}
