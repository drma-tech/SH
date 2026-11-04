using SH.Shared.Models.Property;
using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Anonymous;

public class RoomPublicApi(IHttpClientFactory http) : ApiCosmos<RoomModel>(http, ApiType.Anonymous, null, [], ApiContext.Default.RoomModel)
{
    public async Task<RoomModel?> View(string? roomId, RenderControlState<RoomModel?>[] states, CancellationToken cancellationToken)
    {
        if (roomId.Empty()) return null;

        return await GetAsync($"room/view/{roomId}", setNewVersion: false, states, cancellationToken);
    }
}