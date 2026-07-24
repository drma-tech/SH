using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using SH.API.Repository.Core;
using System.Linq.Expressions;
using System.Net;

namespace SH.API.Repository;

public class CosmosPropertyOffRepository
{
    private readonly ILogger<CosmosPropertyOffRepository> _logger;

    public CosmosPropertyOffRepository(CosmosClient CosmosClient, ILogger<CosmosPropertyOffRepository> logger)
    {
        _logger = logger;

        var databaseId = ApiStartup.Configurations.CosmosDB?.DatabaseId;

        Container = CosmosClient.GetContainer(databaseId, "property-off");
    }

    public Container Container { get; }

    public async Task<T?> Get<T>(DocumentType type, string? id, CancellationToken cancellationToken)
        where T : CosmosDocument
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await Container.ReadItemAsync<T>($"{type}:{id}", new PartitionKey($"{type}:{id}"), null, cancellationToken);

            if (response.RequestCharge > 3)
                _logger.LogWarning("Get - ID {Id}, RequestCharge {Charges}", id, response.RequestCharge);

            return response.Resource;
        }
        catch (CosmosOperationCanceledException)
        {
            return null;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<List<T>> Query<T>(DocumentType type, Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IQueryable<T>>? transform, CancellationToken cancellationToken)
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

            if (charges > 30)
                _logger.LogWarning("Query - Type {Type}, RequestCharge {Charges}", type.ToString(), charges);

            return results;
        }
        catch (CosmosOperationCanceledException)
        {
            return [];
        }
    }

    public async Task<T> CreateItemAsync<T>(T item, CancellationToken cancellationToken)
        where T : CosmosDocument, new()
    {
        try
        {
            var response = await Container.CreateItemAsync(item, new PartitionKey(item.Id), null, cancellationToken);

            if (response.RequestCharge > 20)
                _logger.LogWarning("CreateItemAsync - ID {Id}, RequestCharge {Charges}", item.Id, response.RequestCharge);

            return response.Resource;
        }
        catch (CosmosOperationCanceledException)
        {
            return new T();
        }
    }

    public async Task<T> ReplaceItemAsync<T>(T item, CancellationToken cancellationToken)
        where T : CosmosDocument, new()
    {
        try
        {
            var response = await Container.ReplaceItemAsync(item, item.Id, new PartitionKey(item.Id), null, cancellationToken);

            if (response.RequestCharge > 20)
                _logger.LogWarning("UpsertItemAsync - ID {Id}, RequestCharge {Charges}", item.Id, response.RequestCharge);

            return response.Resource;
        }
        catch (CosmosOperationCanceledException)
        {
            return new T();
        }
    }

    public async Task<bool> Delete<T>(string id, CancellationToken cancellationToken)
        where T : CosmosDocument
    {
        try
        {
            var response = await Container.DeleteItemAsync<T>(id, new PartitionKey(id), null, cancellationToken);

            if (response.RequestCharge > 20)
                _logger.LogWarning("Delete - ID {Id}, RequestCharge {Charges}", id, response.RequestCharge);

            return response.StatusCode == HttpStatusCode.OK;
        }
        catch (CosmosOperationCanceledException)
        {
            return false;
        }
    }

    //Overview of indexing in Azure Cosmos DB
    //https://learn.microsoft.com/en-us/azure/cosmos-db/index-overview
}