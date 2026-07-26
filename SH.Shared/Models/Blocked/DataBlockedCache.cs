namespace SH.Shared.Models.Blocked;

public class DataBlockedCache(string id, DataBlocked data, TtlCache ttl) : CacheDocumentData<DataBlocked>(new CacheIdentity(id), data, ttl)
{
}

public class DataBlocked
{
    public int Quantity { get; set; } = 1;
}