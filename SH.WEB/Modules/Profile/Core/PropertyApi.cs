using SH.Shared.Models.Property;

namespace SH.WEB.Modules.Profile.Core;

public class PropertyApi(IHttpClientFactory http) : ApiCosmos<PropertyModel>(http, ApiType.Authenticated, "property", ["profile"], ApiContext.Default.PropertyModel)
{
    public async Task<PropertyModel?> Get(bool live, string? rawId, RenderControlState<PropertyModel>? actions, CancellationToken cancellationToken)
    {
        if (rawId.Empty()) return null;

        return await GetAsync(Endpoint.Get(live, rawId), setNewVersion: false, actions, cancellationToken);
    }

    public async Task<PropertyModel> Add(PropertyModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync(Endpoint.Add, model, ApiContext.Default.PropertyModel, cancellationToken);
    }

    public async Task<PropertyModel> Update(PropertyModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PutAsync(Endpoint.Update, model, ApiContext.Default.PropertyModel, cancellationToken);
    }

    public async Task<ProfileModel> Delete(bool live, string? rawId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(rawId);

        return await PostAsync(Endpoint.Delete(live, rawId), null, ApiContext.Default.ProfileModel, ApiContext.Default.ProfileModel, cancellationToken);
    }

    public struct Endpoint
    {
        public static string Get(bool live, string rawId) => $"property/get/{live.ToString().ToSlug()}/{rawId}";

        public static string Delete(bool live, string rawId) => $"property/delete/{live.ToString().ToSlug()}/{rawId}";

        public const string Add = "property/add";
        public const string Update = "property/update";
    }
}