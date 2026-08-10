using SH.Shared.Models.Property;

namespace SH.WEB.Modules.Profile.Core;

public class RoomApi(IHttpClientFactory http) : ApiCosmos<RoomModel>(http, ApiType.Authenticated, "room", ["property", "profile"], ApiContext.Default.RoomModel)
{
    public async Task<RoomModel?> Get(bool live, string? rawId, RenderControlState<RoomModel>? actions, CancellationToken cancellationToken)
    {
        if (rawId.Empty()) return null;

        return await GetAsync(Endpoint.Get(live, rawId), setNewVersion: false, actions, cancellationToken);
    }

    public async Task<RoomModel> Add(bool live, string propertyId, RoomModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync(Endpoint.Add(live, propertyId), model, ApiContext.Default.RoomModel, cancellationToken);
    }

    public async Task<RoomModel> Update(bool live, string propertyId, RoomModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PutAsync(Endpoint.Update(live, propertyId), model, ApiContext.Default.RoomModel, cancellationToken);
    }

    public async Task<PropertyModel> Delete(bool live, string? properyId, string? roomId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(properyId);
        ArgumentNullException.ThrowIfNull(roomId);

        return await PostAsync(Endpoint.Delete(live, properyId, roomId), null, ApiContext.Default.PropertyModel, ApiContext.Default.PropertyModel, cancellationToken);
    }

    public struct Endpoint
    {
        public static string Get(bool live, string rawId) => $"room/get/{live.ToString().ToSlug()}/{rawId}";

        public static string Add(bool live, string propertyId) => $"room/add/{live.ToString().ToSlug()}/{propertyId}";

        public static string Update(bool live, string propertyId) => $"room/update/{live.ToString().ToSlug()}/{propertyId}";

        public static string Delete(bool live, string properyId, string roomId) => $"room/delete/{live.ToString().ToSlug()}/{properyId}/{roomId}";
    }
}