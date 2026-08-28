using SH.Shared.Models.Auth;
using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Authenticated;

public class LoginApi(IHttpClientFactory factory) : ApiCosmos<AuthLogin>(factory, ApiType.Authenticated, key: null, [], ApiContext.Default.AuthLogin)
{
    public async Task<AuthLogin?> Get(CancellationToken cancellationToken)
    {
        return await GetAsync("login/get", setNewVersion: true, states: [], cancellationToken);
    }

    public async Task Add(Platform platform, string? country, CancellationToken cancellationToken)
    {
        await PostAsync($"login/add?platform={platform}&country={country ?? "error"}", cancellationToken);
    }
}