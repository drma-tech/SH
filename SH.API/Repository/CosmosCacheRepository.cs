using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using SH.API.Repository.Core;

namespace SH.API.Repository;

public class CosmosCacheRepository(CosmosClient CosmosClient, ILogger<CosmosCacheRepository> logger)
     : BaseRepository<CosmosCacheRepository, CacheDocument, CacheIdentity>(CosmosClient, logger, "cache")
{
}