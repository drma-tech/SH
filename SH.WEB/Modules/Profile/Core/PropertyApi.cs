using SH.Shared.Models.Property;
using SH.Shared.Requests;

namespace SH.WEB.Modules.Profile.Core;

public class PropertyApi(IHttpClientFactory http) : ApiCosmos<PropertyModel>(http, ApiType.Authenticated, "property", ["profile"], ApiContext.Default.PropertyModel)
{
    public async Task<PropertyModel?> Get(bool live, string? rawId, RenderControlState<PropertyModel>? actions, CancellationToken cancellationToken)
    {
        if (rawId.Empty()) return null;

        return await GetAsync(Endpoint.Get(live, rawId), setNewVersion: false, actions, cancellationToken);
    }

    public async Task<IReadOnlyCollection<PropertyModel>> Search(PropertySearchRequest request, RenderControlState<IReadOnlyCollection<PropertyModel>>? actions, CancellationToken cancellationToken)
    {
        return await PostAsync(Endpoint.Search, request, ApiContext.Default.PropertySearchRequest, ApiContext.Default.IReadOnlyCollectionPropertyModel, actions, cancellationToken);
    }

    public async Task<PropertyModel> Add(PropertyModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync(Endpoint.Add, model, ApiContext.Default.PropertyModel, actions: null, cancellationToken);
    }

    public async Task<PropertyModel> Update(PropertyModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PutAsync(Endpoint.Update, model, ApiContext.Default.PropertyModel, actions: null, cancellationToken);
    }

    public async Task<ProfileModel> Delete(string? rawId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(rawId);

        return await PostAsync(Endpoint.Delete(rawId), null, ApiContext.Default.ProfileModel, ApiContext.Default.ProfileModel, actions: null, cancellationToken);
    }

    public async Task<ProfileModel> Public(string id, RenderControlState<ProfileModel>? actions, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(id);

        return await PostAsync(Endpoint.Public(id), null, ApiContext.Default.ProfileModel, ApiContext.Default.ProfileModel, actions, cancellationToken);
    }

    public async Task<ProfileModel> Private(string id, RenderControlState<ProfileModel>? actions, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(id);

        return await PostAsync(Endpoint.Private(id), null, ApiContext.Default.ProfileModel, ApiContext.Default.ProfileModel, actions, cancellationToken);
    }

    public struct Endpoint
    {
        public static string Get(bool live, string rawId) => $"property/get/{live.ToString().ToSlug()}/{rawId}";

        public static string Delete(string rawId) => $"property/delete/{rawId}";

        public static string Public(string rawId) => $"property/public/{rawId}";

        public static string Private(string rawId) => $"property/private/{rawId}";

        public const string Search = "property/search";
        public const string Add = "property/add";
        public const string Update = "property/update";
    }
}