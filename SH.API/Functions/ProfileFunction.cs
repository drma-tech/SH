using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.API.Core.Auth;

namespace SH.API.Functions;

public class ProfileFunction(CosmosRepository repo)
{
    [Function("ProfileGet")]
    public async Task<HttpResponseData?> ProfileGet(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "profile/get")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync(cancellationToken);
        if (userId.Empty()) throw new InvalidOperationException("userId null");

        var model = await repo.Get<ProfileModel>(DocumentType.Profile, userId, cancellationToken);

        return await req.CreateResponse(model, TtlCache.OneDay, cancellationToken);
    }
}