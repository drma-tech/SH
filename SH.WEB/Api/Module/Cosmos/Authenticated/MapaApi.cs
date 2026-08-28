using SH.WEB.Api.Core;
using System.Globalization;

namespace SH.WEB.Api.Module.Cosmos.Authenticated;

public class MapApi(IHttpClientFactory http) : ApiCosmos<HereJson>(http, ApiType.Anonymous, null, [], ApiContext.Default.HereJson)
{
    public async Task<HereJson?> HereBrowser(double Latitude, double Longitude, CancellationToken cancellationToken)
    {
        return await GetAsync($"location/here/browser?latitude={Latitude.ToString(CultureInfo.InvariantCulture)}&longitude={Longitude.ToString(CultureInfo.InvariantCulture)}", setNewVersion: false, states: [], cancellationToken);
    }

    public async Task<HereJson?> HereGeocode(string? search, CancellationToken cancellationToken)
    {
        if (search.Empty()) throw new ArgumentNullException(nameof(search));

        return await GetAsync($"location/here/geocode?search={search}", setNewVersion: false, states: [], cancellationToken);
    }
}