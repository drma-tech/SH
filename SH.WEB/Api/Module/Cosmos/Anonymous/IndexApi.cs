using SH.Shared.Models.Index;
using SH.WEB.Api.Core;

namespace SH.WEB.Api.Module.Cosmos.Anonymous;

public class IndexCountryApi(IHttpClientFactory http) : ApiCosmos<CountryIndex>(http, ApiType.Anonymous, "index", [], ApiContext.Default.CountryIndex)
{
    public async Task<CountryIndex?> GetCountry(RenderControlState<CountryIndex?>[] states, CancellationToken cancellationToken)
    {
        return await GetAsync("index/country", setNewVersion: false, states, cancellationToken);
    }
}

public class IndexStateApi(IHttpClientFactory http) : ApiCosmos<StateIndex>(http, ApiType.Anonymous, "index", [], ApiContext.Default.StateIndex)
{
    public async Task<StateIndex?> GetState(string? country, RenderControlState<StateIndex?>[] states, CancellationToken cancellationToken)
    {
        if (country.Empty()) return null;

        return await GetAsync($"index/state/{country}", setNewVersion: false, states, cancellationToken);
    }
}