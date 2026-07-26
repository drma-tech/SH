namespace SH.Shared.Core.Models;

using Json = System.Text.Json.Serialization;
using Nsoft = Newtonsoft.Json;

public readonly record struct CacheIdentity(string RawId) : ICosmosIdentity
{
    public string Id => RawId;
    public object Key => Id;
}

public class CacheDocument(CacheIdentity identity, TtlCache ttl) : CosmosDocument(identity)
{
    [Json.JsonInclude]
    public TtlCache Ttl { get; init; } = ttl;

    [Json.JsonIgnore]
    [Nsoft.JsonIgnore]
    public new CacheIdentity Identity => new(Id);
}

public class CacheDocumentData<T>(CacheIdentity identity, T? data, TtlCache ttl) : CacheDocument(identity, ttl) where T : class
{
    [Json.JsonInclude]
    public T? Data { get; init; } = data;
}