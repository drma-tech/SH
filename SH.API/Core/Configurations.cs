namespace SH.API.Core;

public class Configurations
{
    public CosmosDB? CosmosDB { get; set; }
    public Azure? Azure { get; set; }
    public Apple? Apple { get; set; }
    public Stripe? Stripe { get; set; }
    public Here? Here { get; set; }
    public Didit? Didit { get; set; }
    public ClerkAuth? ClerkAuth { get; set; }
    public ZeptoMail? ZeptoMail { get; set; }
    public bool IsMaintenanceMode { get; set; }
}

public class CosmosDB
{
    public string? DatabaseId { get; set; }
    public string? ConnectionString { get; set; }
}

public class Azure
{
    public string? BlobConnectionString { get; set; }
    public string? CognitiveKey { get; set; }
    public string? CognitiveEndpoint { get; set; }
}

public class Apple
{
    public string? Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// do not share with users
    /// </summary>
    public string? SharedSecret { get; set; } = string.Empty;

    public string? BundleId { get; set; }
    public ProductSettings? Premium { get; set; } = new();
}

public class Stripe
{
    /// <summary>
    /// do not share with users
    /// </summary>
    public string? ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// do not share with users
    /// </summary>
    public string? SigningSecret { get; set; }

    public ProductSettings? Premium { get; set; } = new();
}

public class ProductSettings
{
    public string? PriceMonth { get; set; }
    public string? PriceYear { get; set; }
}

public class Here
{
    public string? ApiKey { get; set; }
}

public class Didit
{
    public string? ApiKey { get; set; }
    public string? WorkflowId { get; set; }
    public string? SecretKey { get; set; }
}

public class ClerkAuth
{
    public string? SecretKey { get; set; }
}

public class ZeptoMail
{
    public string? ApiKey { get; set; }
}
