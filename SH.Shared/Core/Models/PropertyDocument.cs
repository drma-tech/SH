using SH.Shared.Core.Types;

namespace SH.Shared.Core.Models;

public readonly record struct PropertyIdentity(PropertyType Type, string? DocId) : ICosmosIdentity
{
    public string Id => $"{Type}:{DocId?.RemovePrefix()}";
    public string? RawId => DocId?.RemovePrefix();
    public object Key => Id;
}

public abstract class PropertyDocument(PropertyIdentity identity) : CosmosDocument(identity)
{
    public PropertyType Type { get; set; } = identity.Type;
}