using SH.Shared.Models.Property;
using SH.Shared.Requests;

namespace SH.WEB.Modules.Profile.Core;

public class PropertyApi(IHttpClientFactory http) : ApiCosmos<PropertyModel>(http, ApiType.Authenticated, "property", ["profile"], ApiContext.Default.PropertyModel)
{
    public async Task<PropertyModel?> Get(string? propertyId, RenderControlState<PropertyModel>? actions, CancellationToken cancellationToken)
    {
        if (propertyId.Empty()) return null;

        return await GetAsync(Endpoint.Get(propertyId), setNewVersion: false, actions, cancellationToken);
    }

    public async Task<PropertyModel?> View(string? propertyId, RenderControlState<PropertyModel>? actions, CancellationToken cancellationToken)
    {
        if (propertyId.Empty()) return null;

        return await GetAsync(Endpoint.View(propertyId), setNewVersion: false, actions, cancellationToken);
    }

    public async Task<IReadOnlyCollection<PropertyModel>> Search(PropertySearchRequest request, RenderControlState<IReadOnlyCollection<PropertyModel>>? actions, CancellationToken cancellationToken)
    {
        return await PostAsync(Endpoint.Search, request, ApiContext.Default.PropertySearchRequest, ApiContext.Default.IReadOnlyCollectionPropertyModel, actions, cancellationToken);
    }

    public async Task<PropertyModel> Add(PropertyModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync(Endpoint.Add, model, ApiContext.Default.PropertyModel, state: null, cancellationToken);
    }

    public async Task<PropertyModel> Update(PropertyModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PutAsync(Endpoint.Update, model, ApiContext.Default.PropertyModel, state: null, cancellationToken);
    }

    public async Task<ProfileModel> Delete(string? propertyId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);

        return await PostAsync(Endpoint.Delete(propertyId), null, ApiContext.Default.ProfileModel, ApiContext.Default.ProfileModel, state: null, cancellationToken);
    }

    public async Task<ProfileModel> Public(string propertyId, RenderControlState<ProfileModel>? actions, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);

        return await PostAsync(Endpoint.Public(propertyId), null, ApiContext.Default.ProfileModel, ApiContext.Default.ProfileModel, actions, cancellationToken);
    }

    public async Task<ProfileModel> Private(string propertyId, RenderControlState<ProfileModel>? actions, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);

        return await PostAsync(Endpoint.Private(propertyId), null, ApiContext.Default.ProfileModel, ApiContext.Default.ProfileModel, actions, cancellationToken);
    }

    public struct Endpoint
    {
        public static string Get(string propertyId) => $"property/get/{propertyId}";

        public static string View(string propertyId) => $"property/view/{propertyId}";

        public static string Delete(string propertyId) => $"property/delete/{propertyId}";

        public static string Public(string propertyId) => $"property/public/{propertyId}";

        public static string Private(string propertyId) => $"property/private/{propertyId}";

        public const string Search = "property/search";
        public const string Add = "property/add";
        public const string Update = "property/update";
    }
}