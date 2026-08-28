using SH.Shared.Models.Property;
using SH.Shared.Requests;
using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Anonymous;

public class PropertyPublicApi(IHttpClientFactory http) : ApiCosmos<PropertyModel>(http, ApiType.Anonymous, null, [], ApiContext.Default.PropertyModel)
{
    public async Task<PropertyModel?> View(string? propertyId, RenderControlState<PropertyModel?>[] states, CancellationToken cancellationToken)
    {
        if (propertyId.Empty()) return null;

        return await GetAsync($"property/view/{propertyId}", setNewVersion: false, states, cancellationToken);
    }

    public async Task<IReadOnlyCollection<PropertyModel>> Search(PropertySearchRequest request, RenderControlState<IReadOnlyCollection<PropertyModel>>[] states, CancellationToken cancellationToken)
    {
        return await PostListAsync("property/search", request, ApiContext.Default.PropertySearchRequest, ApiContext.Default.IReadOnlyCollectionPropertyModel, states, cancellationToken);
    }
}