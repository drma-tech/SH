using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Authenticated;

public class ProfileApi(IHttpClientFactory http) : ApiCosmos<ProfileModel>(http, ApiType.Authenticated, "profile", [], ApiContext.Default.ProfileModel)
{
    public async Task<ProfileModel?> Get(RenderControlState<ProfileModel?>[] states, bool setNewVersion, CancellationToken cancellationToken)
    {
        return await GetAsync("profile/get", setNewVersion, states, cancellationToken);
    }

    public async Task<ProfileModel?> DeleteProperty(string? propertyId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);

        return await PostAsync($"property/delete/{propertyId}", null, states: [], cancellationToken);
    }

    public async Task<ProfileModel?> PropertyPublic(string propertyId, RenderControlState<ProfileModel?>[] states, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);

        return await PostAsync($"property/public/{propertyId}", null, states, cancellationToken);
    }

    public async Task<ProfileModel?> PropertyPrivate(string propertyId, RenderControlState<ProfileModel?>[] states, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);

        return await PostAsync($"property/private/{propertyId}", null, states, cancellationToken);
    }
}