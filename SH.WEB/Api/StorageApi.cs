using SH.Shared.Models.Property;
using SH.Shared.Requests;

namespace SH.WEB.Api;

public struct StorageEndpoint
{
    public const string UploadPhotoProperty = "storage/upload-photo/property";
    public const string UploadPhotoRoom = "storage/upload-photo/room";
    public const string DeletePhotoProperty = "storage/delete-photo/property";
    public const string DeletePhotoRoom = "storage/delete-photo/room";
}

public class StoragePropertyApi(IHttpClientFactory factory) : ApiCosmos<PropertyModel>(factory, ApiType.Authenticated, key: null, [], ApiContext.Default.PropertyModel)
{
    public async Task<PropertyModel?> UploadPhotoProperty(PhotoRequest request, CancellationToken cancellationToken)
    {
        SetNewVersion("property", []);
        return await PutAsync(StorageEndpoint.UploadPhotoProperty, request, ApiContext.Default.PhotoRequest, state: null, cancellationToken);
    }

    public async Task<PropertyModel> DeletePhotoGallery(PhotoRequest request, CancellationToken cancellationToken)
    {
        SetNewVersion("property", []);
        return await PutAsync(StorageEndpoint.DeletePhotoProperty, request, ApiContext.Default.PhotoRequest, state: null, cancellationToken);
    }
}

public class StorageRoomApi(IHttpClientFactory factory) : ApiCosmos<RoomModel>(factory, ApiType.Authenticated, key: null, [], ApiContext.Default.RoomModel)
{
    public async Task<RoomModel?> UploadPhotoRoom(PhotoRequest request, CancellationToken cancellationToken)
    {
        SetNewVersion("room", []);
        return await PutAsync(StorageEndpoint.UploadPhotoRoom, request, ApiContext.Default.PhotoRequest, state: null, cancellationToken);
    }

    public async Task<RoomModel> DeletePhotoGallery(PhotoRequest request, CancellationToken cancellationToken)
    {
        SetNewVersion("room", []);
        return await PutAsync(StorageEndpoint.DeletePhotoRoom, request, ApiContext.Default.PhotoRequest, state: null, cancellationToken);
    }
}