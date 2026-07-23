using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.API.Core.Auth;
using SH.Shared.Models.Property;

namespace SH.API.Functions;

public class PropertyFunction(CosmosRepository repo, CosmosPropertyOffRepository repoOff)
{
    [Function("PropertyGet")]
    public async Task<HttpResponseData?> PropertyGet(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "property/get/{id}/{isPublic}")] HttpRequestData req, string id, bool isPublic, CancellationToken cancellationToken)
    {
        var model = isPublic ? null : await repoOff.Get(id, cancellationToken);

        return await req.CreateResponse(model, TtlCache.OneDay, cancellationToken);
    }

    [Function("PropertyAdd")]
    public async Task<PropertyModel?> PropertyAdd(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "property/add")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var model = await req.GetPublicBody<PropertyModel>(cancellationToken);

        var profile = await repo.Get<ProfileModel>(DocumentType.Profile, userId, cancellationToken) ?? throw new NotificationException("profie not found");
        var property = new ProfileProperty()
        {
            PropertyId = model.Id,
            Title = model.Title,
            UrlPath = model.Photos.FirstOrDefault()?.UrlPath,
            Role = PropertyUserRole.Owner
        };

        profile.Properties.Add(property);

        await repo.ReplaceItemAsync(profile, cancellationToken);
        return await repoOff.CreateItemAsync(model, cancellationToken);
    }

    [Function("PropertyUpdate")]
    public async Task<PropertyModel?> PropertyUpdate(
       [HttpTrigger(AuthorizationLevel.Anonymous, Method.Put, Route = "property/update")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("unauthenticated user");

        var model = await req.GetPublicBody<PropertyModel>(cancellationToken);

        var profile = await repo.Get<ProfileModel>(DocumentType.Profile, userId, cancellationToken) ?? throw new NotificationException("Profile not found");

        //todo: change properties of all members
        var property = profile.Properties.SingleOrDefault(p => p.PropertyId == model.Id) ?? throw new InvalidOperationException("Your profile is not linked to this property.");

        property.Title = model.Title;
        property.UrlPath = model.Photos.FirstOrDefault()?.UrlPath;

        await repo.ReplaceItemAsync(profile, cancellationToken);
        return await repoOff.ReplaceItemAsync(model, cancellationToken);
    }
}