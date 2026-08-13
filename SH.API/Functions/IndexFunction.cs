using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SH.Shared.Core.Types;
using SH.Shared.Models.Index;

namespace SH.API.Functions;

public class IndexFunction(CosmosMainRepository repo)
{
    [Function("IndexCountry")]
    public async Task<CountryIndex?> IndexCountry(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "index/country")] HttpRequestData req, CancellationToken cancellationToken)
    {
        return await repo.ReadItemAsync<CountryIndex>(new MainIdentity(MainType.CountryIndex, "all"), cancellationToken);
    }

    [Function("IndexState")]
    public async Task<StateIndex?> IndexState(
        [HttpTrigger(AuthorizationLevel.Anonymous, Method.Get, Route = "index/state/{country}")] HttpRequestData req, string country, CancellationToken cancellationToken)
    {
        return await repo.ReadItemAsync<StateIndex>(new MainIdentity(MainType.StateIndex, country), cancellationToken);
    }
}