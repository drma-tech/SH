using SH.Shared.Models.Property;

namespace SH.WEB.Modules.Profile.Core;

public class PropertyApi(IHttpClientFactory http) : ApiCosmos<PropertyModel>(http, ApiType.Authenticated, "property", ["profile"], ApiContext.Default.PropertyModel)
{
    public async Task<PropertyModel?> Get(string? id, bool isPublic, ComponentActions<PropertyModel>? actions, CancellationToken cancellationToken)
    {
        if (id.Empty()) return null;

        return await GetAsync(Endpoint.Get(id, isPublic), false, actions, cancellationToken);
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

    public struct Endpoint
    {
        public static string Get(string id, bool isPublic) => $"property/get/{id}/{isPublic.ToString().ToSlug()}";

        public const string Add = "property/add";
        public const string Update = "property/update";
    }
}