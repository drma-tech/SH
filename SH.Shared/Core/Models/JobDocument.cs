using SH.Shared.Core.Types;

namespace SH.Shared.Core.Models;

using Json = System.Text.Json.Serialization;
using Nsoft = Newtonsoft.Json;

public readonly record struct JobIdentity(JobType Type, string? RawId) : ICosmosIdentity
{
    public string Id => $"{Type}:{RawId.RemovePrefix()}";
    public object Key => (int)Type;
}

public abstract class JobDocument(JobIdentity identity, DateTimeOffset runAt) : CosmosDocument(identity)
{
    public JobType Type { get; set; } = identity.Type;

    public DateTimeOffset RunAt { get; set; } = runAt;

    [Json.JsonIgnore]
    [Nsoft.JsonIgnore]
    public new JobIdentity Identity => new(Type, Id.RemovePrefix());
}