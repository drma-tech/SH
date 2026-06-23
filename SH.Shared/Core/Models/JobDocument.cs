namespace SH.Shared.Core.Models;

public enum JobType
{
    GoPublic = 1,
}

public abstract class JobDocument : CosmosDocument
{
    protected JobDocument(JobType type)
    {
        Type = type;
    }

    protected JobDocument(string id, JobType type) : base($"{type}:{id}")
    {
        Type = type;
    }

    public JobType Type { get; set; }
    public DateTimeOffset RunAt { get; set; }

    public virtual void Initialize(string id)
    {
        SetIds($"{Type}:{id}");
    }
}
