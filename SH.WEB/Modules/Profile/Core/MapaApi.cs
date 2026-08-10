using System.Globalization;

namespace SH.WEB.Modules.Profile.Core;

public class MapApi(IHttpClientFactory http) : ApiCosmos<HereJson>(http, ApiType.Anonymous, null, [], ApiContext.Default.HereJson)
{
    public async Task<HereJson?> HereBrowser(double Latitude, double Longitude, CancellationToken cancellationToken)
    {
        return await GetAsync(MapEndpoint.HereBrowser(Latitude, Longitude), setNewVersion: false, actions: null, cancellationToken);
    }

    public async Task<HereJson?> HereGeocode(string? search, CancellationToken cancellationToken)
    {
        if (search.Empty()) throw new ArgumentNullException(nameof(search));

        return await GetAsync(MapEndpoint.HereGeocode(search), setNewVersion: false, actions: null, cancellationToken);
    }

    public struct MapEndpoint
    {
        public static string HereBrowser(double Latitude, double Longitude)
        {
            return $"location/here/browser/{Latitude.ToString(CultureInfo.InvariantCulture)}/{Longitude.ToString(CultureInfo.InvariantCulture)}";
        }

        public static string HereGeocode(string? search)
        {
            return $"location/here/geocode?search={search}";
        }
    }
}