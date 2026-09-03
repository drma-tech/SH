using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using SH.API.Core.Auth;
using SH.API.Repository.Core;
using SH.Shared.Core.Types;
using SH.Shared.Models.Property;
using SH.Shared.Requests;
using static SH.Shared.Core.Helper.ImageHelper;

namespace SH.API.Functions.Auth;

public class StorageFunction(CosmosMainRepository repoMain, [FromKeyedServices("Off")] CosmosPropertyRepository repoOff, [FromKeyedServices("On")] CosmosPropertyRepository repoOn, StorageHelper storageHelper)
{
    private CosmosPropertyRepository GetRepo(bool isPublic)
    {
        return isPublic ? repoOn : repoOff;
    }

    [Function("StorageUploadPhotoProperty")]
    public async Task<PropertyModel> StorageUploadPhotoProperty(
        [HttpTrigger(AuthorizationLevel.Function, Method.Post, Route = "storage/upload-photo/property")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var request = await req.GetRequest<PhotoRequest>(cancellationToken);

        var isPublic = await req.IsPropertyPublic(repoMain, request.PropertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var property = await repo.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, request.PropertyId), cancellationToken) ?? throw new NotificationException("Profile not found");

        using var stream1 = new MemoryStream(request.Buffer);

        var photoId = Guid.NewGuid().ToString();

        property.Photos.Add(new PropertyPhoto { PhotoId = photoId, Caption = request.Caption });

        if (property.Photos.Count == 1) //add photo to profile if it is the first one
        {
            var userId = await req.GetUserIdAsync();
            var profile = await repoMain.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("profie not found");

            var profProperty = profile.Properties.Single(p => string.Equals(p.PropertyId, request.PropertyId, StringComparison.Ordinal));

            profProperty.PhotoId = photoId;

            await repoMain.UpsertItemAsync(profile);
        }

        using var stream2 = new MemoryStream(request.Buffer);

        await storageHelper.UploadPhoto(PhotoType.Gallery, stream2, $"{photoId}.jpg", request.PropertyId, cancellationToken);

        return await repo.UpsertItemAsync(property);
    }

    [Function("StorageUploadPhotoRoom")]
    public async Task<RoomModel> StorageUploadPhotoRoom(
        [HttpTrigger(AuthorizationLevel.Function, Method.Post, Route = "storage/upload-photo/room")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var request = await req.GetRequest<PhotoRequest>(cancellationToken);

        var isPublic = await req.IsPropertyPublic(repoMain, request.PropertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var room = await repo.ReadItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, request.RoomId), cancellationToken) ?? throw new NotificationException("Room not found");

        using var stream1 = new MemoryStream(request.Buffer);

        var photoId = Guid.NewGuid().ToString();

        room.Photos.Add(new RoomPhoto { PhotoId = photoId, Caption = request.Caption });

        if (room.Photos.Count == 1) //add photo to property if it is the first one
        {
            var property = await repo.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, request.PropertyId), cancellationToken) ?? throw new NotificationException("Property not found");

            var roomShort = property.Rooms.Single(p => string.Equals(p.RoomId, request.RoomId, StringComparison.Ordinal));

            roomShort.PhotoId = photoId;

            await repo.UpsertItemAsync(property);
        }

        using var stream2 = new MemoryStream(request.Buffer);

        await storageHelper.UploadPhoto(PhotoType.Gallery, stream2, $"{photoId}.jpg", request.RoomId!, cancellationToken);

        return await repo.UpsertItemAsync(room);
    }

    [Function("StorageDeletePhotoProperty")]
    public async Task<PropertyModel> StorageDeletePhotoProperty(
        [HttpTrigger(AuthorizationLevel.Function, Method.Post, Route = "storage/delete-photo/property")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var request = await req.GetRequest<PhotoRequest>(cancellationToken);

        var isPublic = await req.IsPropertyPublic(repoMain, request.PropertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var property = await repo.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, request.PropertyId), cancellationToken) ?? throw new NotificationException("Profile not found");

        await storageHelper.DeletePhoto(PhotoType.Gallery, $"{request.PhotoId}.jpg", cancellationToken);

        property.Photos.Remove(new PropertyPhoto { PhotoId = request.PhotoId });

        return await repo.UpsertItemAsync(property);
    }

    [Function("StorageDeletePhotoRoom")]
    public async Task<RoomModel> StorageDeletePhotoRoom(
        [HttpTrigger(AuthorizationLevel.Function, Method.Post, Route = "storage/delete-photo/room")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var request = await req.GetRequest<PhotoRequest>(cancellationToken);

        var isPublic = await req.IsPropertyPublic(repoMain, request.PropertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var property = await repo.ReadItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, request.RoomId), cancellationToken) ?? throw new NotificationException("Room not found");

        await storageHelper.DeletePhoto(PhotoType.Gallery, $"{request.PhotoId}.jpg", cancellationToken);

        property.Photos.Remove(new RoomPhoto { PhotoId = request.PhotoId });

        return await repo.UpsertItemAsync(property);
    }
}