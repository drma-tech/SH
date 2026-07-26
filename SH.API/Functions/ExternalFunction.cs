using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using SH.API.Core.Auth;

namespace SH.API.Functions;

public class ExternalFunction(IHttpClientFactory factory, IConfiguration config)
{
    [Function("External")]
    public async Task<HttpResponseData> External(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "public/external")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var url = req.GetQueryParameters()["url"]?.ConvertFromBase64ToString() ?? throw new UnhandledException("url null");

        var client = factory.CreateClient();

        using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        return await req.CreateResponse(stream, TtlCache.OneDay, cancellationToken);
    }

    [Function("Country")]
    public async Task<HttpResponseData> Country([HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "public/country")] HttpRequestData req, CancellationToken cancellationToken)
    {
        var ip = req.GetUserIP(false);

        var client = factory.CreateClient("ipinfo");

        if (ip.NotEmpty() && ip != "127.0.0.1")
        {
            var result = await client.GetStringAsync($"https://ipinfo.io/{ip}/country", cancellationToken);
            return await req.CreateResponse(result.Trim().ToLower(), TtlCache.OneMinute, cancellationToken);
        }

        return await req.CreateResponse((string?)null, TtlCache.OneMinute, cancellationToken);
    }

    public string? HereApiKey { get; set; } = config.GetValue<string>("Here:ApiKey");

    [Function("HereBrowser")]
    public async Task<HereJson?> HereBrowser(
        [HttpTrigger(AuthorizationLevel.Function, Method.Get, Route = "location/here/browser/{latitude}/{longitude}")] HttpRequestData req, string latitude, string longitude, CancellationToken cancellationToken)
    {
        using var http = new HttpClient();

        return await http.Get<HereJson>($"https://browse.search.hereapi.com/v1/browse?at={latitude},{longitude}&lang=en-US&limit=1&apiKey={HereApiKey}", cancellationToken);
    }

    [Function("HereGeocode")]
    public async Task<HereJson?> HereGeocode(
        [HttpTrigger(AuthorizationLevel.Function, Method.Get, Route = "location/here/geocode")] HttpRequestData req, string search, CancellationToken cancellationToken)
    {
        using var http = new HttpClient();

        return await http.Get<HereJson>($"https://geocode.search.hereapi.com/v1/geocode?q={search}&lang=en-US&limit=5&apiKey={HereApiKey}", cancellationToken);
    }
}