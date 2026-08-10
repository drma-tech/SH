using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using SH.API.Repository.Core;
using SH.Shared.Core.Types;
using System.Linq.Expressions;

namespace SH.API.Repository;

public class CosmosPropertyOnRepository(CosmosClient CosmosClient, ILogger<CosmosPropertyOnRepository> logger)
    : BaseRepository<CosmosPropertyOnRepository, PropertyDocument, PropertyIdentity>(CosmosClient, logger, "property-on")
{
    public async Task<IReadOnlyCollection<T>> Query<T>(PropertyType type, Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IQueryable<T>>? transform, CancellationToken cancellationToken)
        where T : PropertyDocument
    {
        try
        {
            var queryable = Container
                .GetItemLinqQueryable<T>(requestOptions: CosmosRepositoryExtensions.GetQueryRequestOptions())
                .Where(predicate?.Compose(item => item.Type == type, Expression.AndAlso) ?? (item => item.Type == type));

            if (transform != null) queryable = transform(queryable);

            using var iterator = queryable.ToFeedIterator();
            var results = new List<T>();

            double charges = 0;
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync(cancellationToken);
                charges += response.RequestCharge;
                results.AddRange(response.Resource);
            }

            if (charges > 10d + extra)
                LogMessages.RequestCharge(Logger, "Query", type.ToString(), charges);

            return results;
        }
        catch (CosmosOperationCanceledException)
        {
            return [];
        }
    }
}