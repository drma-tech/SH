using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using SH.API.Repository.Core;

namespace SH.API.Repository;

public class CosmosCacheRepository(CosmosClient client, ILogger<CosmosCacheRepository> logger)
     : BaseRepository<CosmosCacheRepository, CacheDocument, CacheIdentity>(client, logger, "cache")
{
}