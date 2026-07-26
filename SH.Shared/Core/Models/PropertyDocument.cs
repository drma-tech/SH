using SH.Shared.Core.Types;

namespace SH.Shared.Core.Models;

using Json = System.Text.Json.Serialization;
using Nsoft = Newtonsoft.Json;

public readonly record struct PropertyIdentity(PropertyType Type, string? RawId) : ICosmosIdentity
{
    public string Id => $"{Type}:{RawId.RemovePrefix()}";
    public object Key => Id;
}

public abstract class PropertyDocument(PropertyIdentity identity) : CosmosDocument(identity)
{
    public PropertyType Type { get; set; } = identity.Type;

    [Json.JsonIgnore]
    [Nsoft.JsonIgnore]
    public new PropertyIdentity Identity => new(Type, Id.RemovePrefix());
}