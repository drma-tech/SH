using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using SH.API.Repository.Core;
using SH.Shared.Core.Types;
using SH.Shared.Models.Property;

namespace SH.API.Functions;

public class RoomFunction(CosmosMainRepository repoMain, [FromKeyedServices("Off")] CosmosPropertyRepository repoOff, [FromKeyedServices("On")] CosmosPropertyRepository repoOn)
{
    private CosmosPropertyRepository GetRepo(bool isPublic)
    {
        return isPublic ? repoOn : repoOff;
    }

    [Function("RoomGet")]
    public async Task<HttpResponseData?> RoomGet(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "room/get/{propertyId}/{roomId}")] HttpRequestData req, string propertyId, string roomId, CancellationToken cancellationToken)
    {
        var isPublic = await req.IsPropertyPublic(repoMain, propertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var model = await repo.ReadItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, roomId), cancellationToken);

        return await req.CreateResponse(model, TtlCache.OneDay, cancellationToken);
    }

    [Function("RoomAdd")]
    public async Task<RoomModel?> RoomAdd(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "room/add/{propertyId}")] HttpRequestData req, string propertyId, CancellationToken cancellationToken)
    {
        var body = await req.GetBody<RoomModel>(cancellationToken);

        var isPublic = await req.IsPropertyPublic(repoMain, propertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var model = await repo.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId), cancellationToken) ?? throw new NotificationException("Property not found");

        var room = new RoomShort()
        {
            RoomId = body.Identity.RawId,
            Title = body.Title,
            Status = body.Status,
            MonthlyPrice = body.GetMonthlyCosts(),
        };

        model.Rooms.Add(room);

        await repo.UpsertItemAsync(model);

        return await repo.CreateItemAsync(body);
    }

    [Function("RoomUpdate")]
    public async Task<RoomModel?> RoomUpdate(
       [HttpTrigger(AuthorizationLevel.Anonymous, Method.Put, Route = "room/update/{propertyId}")] HttpRequestData req, string propertyId, CancellationToken cancellationToken)
    {
        var body = await req.GetBody<RoomModel>(cancellationToken);

        var isPublic = await req.IsPropertyPublic(repoMain, propertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var model = await repo.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId), cancellationToken) ?? throw new NotificationException("Property not found");

        var shortRoom = model.Rooms.Single(p => string.Equals(p.RoomId, body.Identity.RawId, StringComparison.Ordinal));

        shortRoom.Title = body.Title;
        shortRoom.UrlPath = body.Photos.FirstOrDefault()?.UrlPath;
        shortRoom.Status = body.Status;
        shortRoom.MonthlyPrice = body.GetMonthlyCosts();

        await repo.UpsertItemAsync(model);
        return await repo.UpsertItemAsync(body);
    }

    [Function("RoomDelete")]
    public async Task<PropertyModel> RoomDelete(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "room/delete/{propertyId}/{roomId}")] HttpRequestData req, string propertyId, string roomId, CancellationToken cancellationToken)
    {
        var isPublic = await req.IsPropertyPublic(repoMain, propertyId, cancellationToken);
        var repo = GetRepo(isPublic);

        var model = await repo.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, propertyId), cancellationToken) ?? throw new NotificationException("Property not found");

        if (model.Rooms.ToHashSet().RemoveWhere(p => string.Equals(p.RoomId, roomId, StringComparison.Ordinal)) != 1)
        {
            throw new InvalidOperationException("Room not found");
        }

        await repo.DeleteItemAsync<RoomModel>(new PropertyIdentity(PropertyType.Room, roomId));

        return await repo.UpsertItemAsync(model);
    }
}