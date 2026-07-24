namespace SH.Shared.Core.Models;

public enum DocumentType
{
    Principal = 1,
    Login = 2,
    Profile = 3,
    Property = 4,
    Room = 5,
    Proposal = 6,
    Occupation = 7
}

public abstract class MainDocument : CosmosDocument
{
    protected MainDocument(DocumentType type)
    {
        Type = type;
    }

    protected MainDocument(string id, DocumentType type) : base($"{type}:{id}")
    {
        Type = type;
    }

    public DocumentType Type { get; set; }
}

/// <summary>
///     Public read and private write
/// </summary>
public abstract class ProtectedMainDocument(DocumentType type) : MainDocument(type)
{
    private readonly DocumentType _type = type;

    public virtual void Initialize(string id)
    {
        SetIds($"{_type}:{id}");
    }

    public string GetRawId()
    {
        return Id.Split(":")[1];
    }
}

/// <summary>
///     Private read and write
/// </summary>
public abstract class PrivateMainDocument(DocumentType type) : MainDocument(type)
{
    private readonly DocumentType _type = type;

    public virtual void Initialize(string userId)
    {
        SetIds($"{_type}:{userId}");
    }

    public string GetRawId()
    {
        return Id.Split(":")[1];
    }
}