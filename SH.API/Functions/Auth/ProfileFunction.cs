using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.API.Core.Auth;
using SH.Shared.Core.Types;

namespace SH.API.Functions.Auth;

public class ProfileFunction(CosmosMainRepository repo)
{
    [Function("ProfileGet")]
    public async Task<HttpResponseData?> ProfileGet(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "profile/get")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync();

        var model = await repo.ReadItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId), cancellationToken);

        return await req.CreateResponse(model, TtlCache.OneDay, cancellationToken);
    }

    [Function("ProfileUpdate")]
    public async Task<HttpResponseData?> ProfileUpdate(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "profile/update")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var body = await req.GetBody<ProfileModel>(cancellationToken);
        await req.ValidateUser(body.Identity.RawId, cancellationToken);

        var result = await repo.UpsertItemAsync(body);

        return await req.CreateResponse(result, TtlCache.OneHour, cancellationToken);
    }
}