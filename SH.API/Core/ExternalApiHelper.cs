using System.Net.Http.Json;

namespace SH.API.Core;

public static class ExternalApiHelper
{
    public static async Task<MemoryStream> GetImageStreamFromUrlAsync(this HttpClient http, string url, CancellationToken cancellationToken)
    {
        var response = await http.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        var imageBytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        return new MemoryStream(imageBytes);
    }

    public static async Task<T?> Get<T>(this HttpClient http, string requestUri, CancellationToken cancellationToken)
        where T : class
    {
        var response = await http.GetAsync(requestUri, cancellationToken);

        if (!response.IsSuccessStatusCode) throw new UnhandledException(response.ReasonPhrase);

        return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
    }
}
