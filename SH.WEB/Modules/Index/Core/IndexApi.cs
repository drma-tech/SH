using SH.Shared.Models.Index;

namespace SH.WEB.Modules.Index.Core;

public class IndexApi(IHttpClientFactory http) : ApiCosmos<CountryIndex>(http, ApiType.Anonymous, "index", [], ApiContext.Default.CountryIndex)
{
    public async Task<CountryIndex?> GetCountry(RenderControlState<CountryIndex>? actions, CancellationToken cancellationToken)
    {
        return await GetAsync(Endpoint.GetCountry, setNewVersion: false, actions, cancellationToken);
    }

    public async Task<StateIndex?> GetState(string? country, RenderControlState<StateIndex>? actions, CancellationToken cancellationToken)
    {
        if (country.Empty()) return null;

        return await GetAsync(Endpoint.GetState(country), setNewVersion: false, actions, cancellationToken);
    }

    public struct Endpoint
    {
        public const string GetCountry = "index/country";

        public static string GetState(string country) => $"index/state/{country}";

        public static string GetCity(string country, string state) => $"index/city/{country}/{state}";
    }
}