using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.API.Core.Auth;
using SH.Shared.Core.Types;
using SH.Shared.Models.Property;

namespace SH.API.Functions;

public class PropertyFunction(CosmosMainRepository repo, CosmosPropertyOffRepository repoOff)
{
    [Function("PropertyGet")]
    public async Task<HttpResponseData?> PropertyGet(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "property/get/{live}/{id}")] HttpRequestData req, bool live, string id, CancellationToken cancellationToken)
    {
        var model = live ? null : await repoOff.ReadItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, id), cancellationToken);

        return await req.CreateResponse(model, TtlCache.OneDay, cancellationToken);
    }

    [Function("PropertyAdd")]
    public async Task<PropertyModel?> PropertyAdd(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/add")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var model = await req.GetBody<PropertyModel>(cancellationToken);

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("profie not found");
        var property = new ProfileProperty()
        {
            PropertyId = model.Identity.RawId,
            Title = model.Title,
            UrlPath = model.Photos.FirstOrDefault()?.UrlPath,
            Role = PropertyUserRole.Owner
        };

        profile.Properties.Add(property);

        await repo.UpsertItemAsync(profile);
        return await repoOff.CreateItemAsync(model);
    }

    [Function("PropertyUpdate")]
    public async Task<PropertyModel?> PropertyUpdate(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Put, Route = "property/update")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var model = await req.GetBody<PropertyModel>(cancellationToken);

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("Profile not found");

        //todo: change properties of all members
        var property = profile.Properties.SingleOrDefault(p => p.PropertyId == model.Identity.RawId) ?? throw new InvalidOperationException("Your profile is not linked to this property.");

        property.Title = model.Title;
        property.UrlPath = model.Photos.FirstOrDefault()?.UrlPath;

        await repo.UpsertItemAsync(profile);
        return await repoOff.UpsertItemAsync(model);
    }

    [Function("PropertyDelete")]
    public async Task<ProfileModel> PropertyDelete(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/delete/{live}/{id}")] HttpRequestData req, bool live, string id, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var profile = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken) ?? throw new NotificationException("Profile not found");

        //todo: change properties of all members
        if (profile.Properties.RemoveWhere(p => p.PropertyId == id) != 1)
        {
            throw new InvalidOperationException("Your profile is not linked to this property.");
        }

        if (live)
        {
            //todo: implement live repository
        }
        else
        {
            await repoOff.DeleteItemAsync<PropertyModel>(new PropertyIdentity(PropertyType.Property, id));
        }

        return await repo.UpsertItemAsync(profile);
    }
}