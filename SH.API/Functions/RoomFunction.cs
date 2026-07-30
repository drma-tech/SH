using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.API.Core.Auth;
using SH.Shared.Core.Types;
using SH.Shared.Models.Property;

namespace SH.API.Functions;

public class RoomFunction(CosmosMainRepository repo, CosmosPropertyOffRepository repoOff)
{
    [Function("RoomGet")]
    public async Task<HttpResponseData?> RoomGet(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "room/get/{live}/{id}")] HttpRequestData req, bool live, string id, CancellationToken cancellationToken)
    {
        var model = live ? null : await repoOff.ReadItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, id), cancellationToken);

        return await req.CreateResponse(model, TtlCache.OneDay, cancellationToken);
    }

    [Function("RoomAdd")]
    public async Task<RoomModel?> RoomAdd(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "room/add/{live}/{propertyId}")] HttpRequestData req, bool live, string propertyId, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var model = await req.GetBody<RoomModel>(cancellationToken);

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("Profile not found");

        //todo: change properties of all members
        var temp = profile.Properties.SingleOrDefault(p => p.PropertyId == propertyId) ?? throw new InvalidOperationException("Your profile is not linked to this property.");

        var property = live ? throw new NotImplementedException() : await repoOff.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, temp.PropertyId), cancellationToken) ?? throw new NotificationException("Property not found");

        var room = new RoomShort()
        {
            RoomId = model.Identity.RawId,
            Title = model.Title,
            Status = model.Status,
            MonthlyPrice = model.GetMonthlyCosts()
        };

        property.Rooms.Add(room);

        await repoOff.UpsertItemAsync(property);

        return await repoOff.CreateItemAsync(model);
    }

    [Function("RoomUpdate")]
    public async Task<RoomModel?> RoomUpdate(
       [HttpTrigger(AuthorizationLevel.Anonymous, Method.Put, Route = "room/update/{live}/{propertyId}")] HttpRequestData req, bool live, string propertyId, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var model = await req.GetBody<RoomModel>(cancellationToken);

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("Profile not found");

        //todo: change properties of all members
        var temp = profile.Properties.SingleOrDefault(p => p.PropertyId == propertyId) ?? throw new InvalidOperationException("Your profile is not linked to this property.");

        var property = live ? throw new NotImplementedException() : await repoOff.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, temp.PropertyId), cancellationToken) ?? throw new NotificationException("Property not found");

        var shortRoom = property.Rooms.Single(p => p.RoomId == model.Identity.RawId);

        shortRoom.Title = model.Title;
        shortRoom.UrlPath = model.Photos.FirstOrDefault()?.UrlPath;
        shortRoom.Status = model.Status;
        shortRoom.MonthlyPrice = model.GetMonthlyCosts();

        await repoOff.UpsertItemAsync(property);
        return await repoOff.UpsertItemAsync(model);
    }

    [Function("RoomDelete")]
    public async Task<PropertyModel> RoomDelete(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "room/delete/{live}/{propertyId}/{id}")] HttpRequestData req, bool live, string propertyId, string id, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("Profile not found");

        //todo: change properties of all members
        var temp = profile.Properties.SingleOrDefault(p => p.PropertyId == propertyId) ?? throw new InvalidOperationException("Your profile is not linked to this property.");

        var property = live ? throw new NotImplementedException() : await repoOff.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, temp.PropertyId), cancellationToken) ?? throw new NotificationException("Property not found");

        //todo: change properties of all members
        if (property.Rooms.RemoveWhere(p => p.RoomId == id) != 1)
        {
            throw new InvalidOperationException("Your profile is not linked to this property.");
        }

        if (live)
        {
            //todo: implement live repository
        }
        else
        {
            await repoOff.DeleteItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, id));
        }

        return await repoOff.UpsertItemAsync(property);
    }
}