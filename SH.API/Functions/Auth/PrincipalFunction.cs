using Clerk.BackendAPI;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.API.Core.Auth;
using SH.Shared.Core.Types;
using SH.Shared.Models.Auth;

namespace SH.API.Functions.Auth;

public class PrincipalFunction(CosmosMainRepository repo, IHttpClientFactory factory)
{
    [Function("PrincipalGet")]
    public async Task<HttpResponseData?> PrincipalGet(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "principal/get")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync();

        var model = await repo.ReadItemAsync<AuthPrincipal>(new MainIdentity(MainType.Principal, userId), cancellationToken);

        return await req.CreateResponse(model, TtlCache.OneHour, cancellationToken);
    }

    [Function("PrincipalAdd")]
    public async Task<AuthPrincipal?> PrincipalAdd(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "principal/add")] HttpRequestData req, CancellationToken cancellationToken)
    {
        //note: its called once per user (first access)

        var userId = await req.GetUserIdAsync();
        var body = await req.GetBody<AuthPrincipal>(cancellationToken);
        var platform = req.GetQueryParameters()["platform"];
        var country = req.GetQueryParameters()["country"];

        await req.ValidateUser(body.UserId, cancellationToken);

        var ip = req.GetUserIP(includePort: false) ?? throw new UnhandledException("Failed to retrieve IP");

        foreach (var item in body.Events.Where(w => w.Ip.Empty()))
        {
            item.Ip = ip;
        }

        var zepto = new ZeptoMailClient(factory, ApiStartup.Configurations.ZeptoMail!.ApiKey!);
        if (body.Email.NotEmpty()) _ = zepto.SendWelcomeTemplate(userId, body.Email, body.DisplayName, cancellationToken);

        var principal = new AuthPrincipal(userId)
        {
            DisplayName = body.DisplayName,
            Email = body.Email,
            Events = body.Events,
        };

        principal = await repo.CreateItemAsync(principal);

        if (platform.NotEmpty())
        {
            var newLogin = new AuthLogin(userId)
            {
                UserId = userId,
                Accesses = new HashSet<Access> { new() { Date = DateTimeOffset.UtcNow, Platform = platform, Ip = ip, Country = country } },
            };

            await repo.CreateItemAsync(newLogin);
        }

        var countryEnum = country.NotEmpty() ? country.ParseToEnum<Country>() : (Country?)null;

        var profile = new ProfileModel(userId)
        {
            FirstName = body.DisplayName?.Split(" ").ElementAtIndex(0),
            LastName = body.DisplayName?.Split(" ").ElementAtIndex(1),
            Nationality = countryEnum,
            Languages = countryEnum.GetLanguages(),
        };

        await repo.CreateItemAsync(profile);

        return principal;
    }

    [Function("PrincipalEvent")]
    public async Task<AuthPrincipal> PrincipalEvent(
       [HttpTrigger(AuthorizationLevel.Anonymous, Method.Post, Route = "principal/event")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var userId = await req.GetUserIdAsync();
        var ip = req.GetUserIP(includePort: true);

        var principal = await repo.ReadItemAsync<AuthPrincipal>(new MainIdentity(MainType.Principal, userId), cancellationToken) ?? throw new UnhandledException("Client null");

        var app = req.GetQueryParameters()["app"];
        var msg = req.GetQueryParameters()["msg"];

        principal.Events.Add(new Event(app, msg, ip));

        return await repo.UpsertItemAsync(principal);
    }

    [Function("PrincipalRemove")]
    public async Task PrincipalRemove(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Delete, Route = "principal/remove")] HttpRequestData req)
    {
        var userId = await req.GetUserIdAsync();

        await DeleteUser(repo, userId);
    }

    public static async Task DeleteUser(CosmosMainRepository repo, string? userId)
    {
        if (userId.Empty()) throw new UnhandledException("UserId is empty");

        //todo: delete properties and rooms

        await repo.DeleteItemAsync<AuthPrincipal>(new MainIdentity(MainType.Principal, userId));
        await repo.DeleteItemAsync<AuthLogin>(new MainIdentity(MainType.Login, userId));
        await repo.DeleteItemAsync<ProfileModel>(new MainIdentity(MainType.Profile, userId));

        var sdk = new ClerkBackendApi(bearerAuth: ApiStartup.Configurations.ClerkAuth!.SecretKey);
        await sdk.Users.DeleteAsync(userId);
    }
}
