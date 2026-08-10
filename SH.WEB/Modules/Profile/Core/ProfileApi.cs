namespace SH.WEB.Modules.Profile.Core;

public class ProfileApi(IHttpClientFactory http) : ApiCosmos<ProfileModel>(http, ApiType.Authenticated, "profile", [], ApiContext.Default.ProfileModel)
{
    public async Task<ProfileModel?> Get(RenderControlState<ProfileModel> actions, bool setNewVersion, CancellationToken cancellationToken)
    {
        return await GetAsync(Endpoint.Get, setNewVersion, actions, cancellationToken);
    }

    public struct Endpoint
    {
        public const string Get = "profile/get";
    }
}