using SH.Shared.Models.Property;

namespace SH.WEB.Modules.Profile.Core;

public class RoomApi(IHttpClientFactory http) : ApiCosmos<RoomModel>(http, ApiType.Authenticated, "room", ["property", "profile"], ApiContext.Default.RoomModel)
{
    public async Task<RoomModel?> Get(string? propertyId, string? roomId, RenderControlState<RoomModel>? actions, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);
        ArgumentNullException.ThrowIfNull(roomId);

        return await GetAsync(Endpoint.Get(propertyId, roomId), setNewVersion: false, actions, cancellationToken);
    }

    public async Task<RoomModel> Add(string? propertyId, RoomModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync(Endpoint.Add(propertyId), model, ApiContext.Default.RoomModel, state: null, cancellationToken);
    }

    public async Task<RoomModel> Update(string? propertyId, RoomModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);
        ArgumentNullException.ThrowIfNull(model);

        return await PutAsync(Endpoint.Update(propertyId), model, ApiContext.Default.RoomModel, state: null, cancellationToken);
    }

    public async Task<PropertyModel> Delete(string? propertyId, string? roomId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);
        ArgumentNullException.ThrowIfNull(roomId);

        return await PostAsync(Endpoint.Delete(propertyId, roomId), null, ApiContext.Default.PropertyModel, ApiContext.Default.PropertyModel, state: null, cancellationToken);
    }

    public struct Endpoint
    {
        public static string Get(string propertyId, string roomId) => $"room/get/{propertyId}/{roomId}";

        public static string Add(string propertyId) => $"room/add/{propertyId}";

        public static string Update(string propertyId) => $"room/update/{propertyId}";

        public static string Delete(string properyId, string roomId) => $"room/delete/{properyId}/{roomId}";
    }
}