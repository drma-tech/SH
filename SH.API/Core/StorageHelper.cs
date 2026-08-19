using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using static SH.Shared.Core.Helper.ImageHelper;

namespace SH.API.Core;

public class StorageHelper(IConfiguration configuration)
{
    private const string BlobConnectionString = "Azure:BlobConnectionString";

    public IConfiguration Configuration { get; } = configuration;

    public async Task UploadPhoto(PhotoType type, Stream stream, string fileName, string docId, CancellationToken cancellationToken)
    {
        var container = new BlobContainerClient(Configuration.GetValue<string>(BlobConnectionString), GetPhotoContainer(type));
        var client = container.GetBlobClient(fileName);

        var headers = new BlobHttpHeaders { ContentType = "image/jpeg" };

        await client.UploadAsync(stream, headers, new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "id", docId } }, cancellationToken: cancellationToken);
    }

    public async Task DeletePhoto(PhotoType type, string? fileName, CancellationToken cancellationToken)
    {
        if (fileName.Empty()) throw new ArgumentNullException(nameof(fileName));

        var container = new BlobContainerClient(Configuration.GetValue<string>(BlobConnectionString), GetPhotoContainer(type));
        var blob = container.GetBlobClient(fileName);

        if (await blob.ExistsAsync(cancellationToken))
            await blob.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);
    }
}