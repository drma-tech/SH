using SH.Shared.Models.Auth;
using SH.Shared.Models.Property;
using SH.Shared.Models.Subscription;
using System.Text.Json.Serialization;

namespace SH.WEB.Core.Api
{
    [JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
    [JsonSerializable(typeof(bool?))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(Platform?))]
    [JsonSerializable(typeof(AppLanguage?))]
    [JsonSerializable(typeof(AuthProvider))]
    [JsonSerializable(typeof(HashSet<DateTime>))]
    internal partial class JavascriptContext : JsonSerializerContext
    {
    }

    [JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
    [JsonSerializable(typeof(PaymentConfigurations))]
    [JsonSerializable(typeof(AuthPrincipal))]
    [JsonSerializable(typeof(AuthLogin))]
    [JsonSerializable(typeof(AuthSubscription))]
    [JsonSerializable(typeof(HereJson))]
    [JsonSerializable(typeof(PropertyModel))]
    [JsonSerializable(typeof(ProfileModel))]
    internal partial class ApiContext : JsonSerializerContext
    {
    }
}
