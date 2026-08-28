using SH.Shared.Models.Property;
using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Authenticated;

public class PropertyApi(IHttpClientFactory http) : ApiCosmos<PropertyModel>(http, ApiType.Authenticated, "property", ["profile"], ApiContext.Default.PropertyModel)
{
    public async Task<PropertyModel?> Get(string? propertyId, RenderControlState<PropertyModel?>[] states, CancellationToken cancellationToken)
    {
        if (propertyId.Empty()) return null;

        return await GetAsync($"property/get/{propertyId}", setNewVersion: false, states, cancellationToken);
    }

    public async Task<PropertyModel?> Add(PropertyModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync("property/add", model, ApiContext.Default.PropertyModel, states: [], cancellationToken);
    }

    public async Task<PropertyModel?> Update(PropertyModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync("property/update", model, ApiContext.Default.PropertyModel, states: [], cancellationToken);
    }

    public async Task<PropertyModel?> DeleteRoom(string? propertyId, string? roomId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);
        ArgumentNullException.ThrowIfNull(roomId);

        return await PostAsync($"room/delete/{propertyId}/{roomId}", null, states: [], cancellationToken);
    }
}