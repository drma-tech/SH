using SH.Shared.Models.Property;
using SH.Shared.Requests;
using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Authenticated;

public class StoragePropertyApi(IHttpClientFactory factory) : ApiCosmos<PropertyModel>(factory, ApiType.Authenticated, key: "property", [], ApiContext.Default.PropertyModel)
{
    public async Task<PropertyModel?> UploadPhotoProperty(PhotoRequest request, CancellationToken cancellationToken)
    {
        SetNewVersion();
        return await PostAsync("storage/upload-photo/property", request, ApiContext.Default.PhotoRequest, states: [], cancellationToken);
    }

    public async Task<PropertyModel?> DeletePhotoGallery(PhotoRequest request, CancellationToken cancellationToken)
    {
        SetNewVersion();
        return await PostAsync("storage/delete-photo/property", request, ApiContext.Default.PhotoRequest, states: [], cancellationToken);
    }
}

public class StorageRoomApi(IHttpClientFactory factory) : ApiCosmos<RoomModel>(factory, ApiType.Authenticated, key: "room", [], ApiContext.Default.RoomModel)
{
    public async Task<RoomModel?> UploadPhotoRoom(PhotoRequest request, CancellationToken cancellationToken)
    {
        SetNewVersion();
        return await PostAsync("storage/upload-photo/room", request, ApiContext.Default.PhotoRequest, states: [], cancellationToken);
    }

    public async Task<RoomModel?> DeletePhotoGallery(PhotoRequest request, CancellationToken cancellationToken)
    {
        SetNewVersion();
        return await PostAsync("storage/delete-photo/room", request, ApiContext.Default.PhotoRequest, states: [], cancellationToken);
    }
}