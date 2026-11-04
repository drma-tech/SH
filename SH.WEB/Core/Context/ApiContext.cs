using SH.Shared.Models.Auth;
using SH.Shared.Models.Index;
using SH.Shared.Models.Property;
using SH.Shared.Models.Subscription;
using SH.Shared.Requests;
using System.Text.Json.Serialization;

namespace SH.WEB.Core.Context
{
    [JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
    [JsonSerializable(typeof(PaymentConfigurations))]
    [JsonSerializable(typeof(AuthPrincipal))]
    [JsonSerializable(typeof(AuthLogin))]
    [JsonSerializable(typeof(AuthSubscription))]
    [JsonSerializable(typeof(HereJson))]
    [JsonSerializable(typeof(PropertyModel))]
    [JsonSerializable(typeof(IReadOnlyCollection<PropertyModel>))]
    [JsonSerializable(typeof(RoomModel))]
    [JsonSerializable(typeof(ProfileModel))]
    [JsonSerializable(typeof(CountryIndex))]
    [JsonSerializable(typeof(StateIndex))]
    [JsonSerializable(typeof(PropertySearchRequest))]
    [JsonSerializable(typeof(PhotoRequest))]
    [JsonSerializable(typeof(RoomProposal))]
    [JsonSerializable(typeof(RoomOccupation))]
    internal sealed partial class ApiContext : JsonSerializerContext
    {
    }
}