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
    internal sealed partial class JavascriptContext : JsonSerializerContext
    {
    }
}
