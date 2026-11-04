using SH.Shared.Models.Property;
using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Authenticated;

public class RoomApi(IHttpClientFactory http) : ApiCosmos<RoomModel>(http, ApiType.Authenticated, "room", ["property", "profile"], ApiContext.Default.RoomModel)
{
    public async Task<RoomModel?> Get(string? propertyId, string? roomId, RenderControlState<RoomModel?>[] states, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);
        ArgumentNullException.ThrowIfNull(roomId);

        return await GetAsync($"room/get/{propertyId}/{roomId}", setNewVersion: false, states, cancellationToken);
    }

    public async Task<RoomModel?> Add(string? propertyId, RoomModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync($"room/add/{propertyId}", model, ApiContext.Default.RoomModel, states: [], cancellationToken);
    }

    public async Task<RoomModel?> Update(string? propertyId, RoomModel? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(propertyId);
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync($"room/update/{propertyId}", model, ApiContext.Default.RoomModel, states: [], cancellationToken);
    }
}

public class RoomProposalApi(IHttpClientFactory http) : ApiCosmos<RoomProposal>(http, ApiType.Authenticated, "room", ["property", "profile"], ApiContext.Default.RoomProposal)
{
    public async Task<RoomProposal?> Proposal(RoomProposal? model, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(model);

        return await PostAsync($"room/proposal", model, ApiContext.Default.RoomProposal, states: [], cancellationToken);
    }

    public async Task<IEnumerable<RoomProposal>> GetListAsync(string? roomId, RenderControlState<IEnumerable<RoomProposal>>[] states, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(roomId);

        return await GetListAsync($"room/proposal/list/{roomId}", states, cancellationToken);
    }
}