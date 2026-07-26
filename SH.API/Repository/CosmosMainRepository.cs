using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using SH.API.Repository.Core;
using SH.Shared.Core.Types;
using System.Linq.Expressions;

namespace SH.API.Repository;

public class CosmosMainRepository(CosmosClient CosmosClient, ILogger<CosmosMainRepository> logger)
    : BaseRepository<CosmosMainRepository, MainDocument, MainIdentity>(CosmosClient, logger, "main")
{
    public async Task<List<T>> Query<T>(MainType type, Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IQueryable<T>>? transform, CancellationToken cancellationToken)
        where T : MainDocument
    {
        try
        {
            var query = Container
                .GetItemLinqQueryable<T>(requestOptions: CosmosRepositoryExtensions.GetQueryRequestOptions())
                .Where(predicate?.Compose(item => item.Type == type, Expression.AndAlso) ?? (item => item.Type == type));

            if (transform != null) query = transform(query);

            using var iterator = query.ToFeedIterator();
            var results = new List<T>();

            double charges = 0;
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync(cancellationToken);
                charges += response.RequestCharge;
                results.AddRange(response.Resource);
            }

            if (charges > 20)
                _logger.LogWarning("Query - Type {Type}, RequestCharge {Charges}", type.ToString(), charges);

            return results;
        }
        catch (CosmosOperationCanceledException)
        {
            return [];
        }
    }
}