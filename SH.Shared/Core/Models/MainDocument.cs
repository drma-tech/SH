using SH.Shared.Core.Types;

namespace SH.Shared.Core.Models;

using Json = System.Text.Json.Serialization;
using Nsoft = Newtonsoft.Json;

public readonly record struct MainIdentity(MainType Type, string? RawId) : ICosmosIdentity
{
    public string Id => $"{Type}:{RawId.RemovePrefix()}";
    public object Key => Id;
}

public abstract class MainDocument(MainIdentity identity) : CosmosDocument(identity)
{
    public MainType Type { get; set; } = identity.Type;

    [Json.JsonIgnore]
    [Nsoft.JsonIgnore]
    public new MainIdentity Identity => new(Type, Id.RemovePrefix());
}