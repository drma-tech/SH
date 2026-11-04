# Configuration

The backend configuration is defined by the `Configurations` class in `SH.API/Core/Configurations.cs`.

During local development, the API loads configuration from:

1. `SH.API/local.settings.json`
2. .NET User Secrets

The User Secrets provider is added after `local.settings.json`, so matching values from User Secrets take precedence.

## User Secrets

The `SH.API` project already defines a `UserSecretsId`. .NET stores the development secrets outside the repository, in the user profile directory.

To list or edit the secrets for this project, use the .NET CLI:

```bash
dotnet user-secrets list --project SH.API
dotnet user-secrets edit --project SH.API
```

The secrets file contains configuration properties at its root. Do not wrap them in a `Configurations` object or in a `Values` object.

Example structure:

```json
{
  "CosmosDB": {
    "DatabaseId": "your-cosmos-db-database-id",
    "ConnectionString": "your-cosmos-db-connection-string"
  },
  "Azure": {
    "BlobConnectionString": "your-azure-storage-connection-string",
    "CognitiveKey": "your-azure-cognitive-services-key",
    "CognitiveEndpoint": "your-azure-cognitive-services-endpoint"
  },
  "Apple": {
    "Endpoint": "https://buy.itunes.apple.com/",
    "SharedSecret": "your-apple-shared-secret",
    "BundleId": "your-app-bundle-id",
    "Premium": {
      "PriceMonth": "your-monthly-app-store-product-id",
      "PriceYear": "your-yearly-app-store-product-id"
    }
  },
  "Stripe": {
    "ApiKey": "your-stripe-secret-key",
    "SigningSecret": "your-stripe-webhook-signing-secret",
    "Premium": {
      "PriceMonth": "your-monthly-stripe-price-id",
      "PriceYear": "your-yearly-stripe-price-id"
    }
  },
  "Here": {
    "ApiKey": "your-here-api-key"
  },
  "Didit": {
    "ApiKey": "your-didit-api-key",
    "WorkflowId": "your-didit-workflow-id",
    "SecretKey": "your-didit-secret-key"
  },
  "ClerkAuth": {
    "SecretKey": "your-clerk-secret-key",
    "SigningSecret": "your-clerk-webhook-signing-secret"
  },
  "UserJot": {
    "ProjectId": "your-userjot-project-id",
    "Secret": "your-userjot-secret"
  },
  "ZeptoMail": {
    "ApiKey": "your-zeptomail-api-key"
  },
  "IsMaintenanceMode": false
}
```

Replace the placeholders with values from the corresponding service accounts. Do not commit real credentials or share the generated `secrets.json` file.

You can also set individual values through the CLI. For example:

```bash
dotnet user-secrets set "CosmosDB:DatabaseId" "your-database-id" --project SH.API
dotnet user-secrets set "CosmosDB:ConnectionString" "your-connection-string" --project SH.API
dotnet user-secrets set "Azure:BlobConnectionString" "your-storage-connection-string" --project SH.API
dotnet user-secrets set "ClerkAuth:SecretKey" "your-clerk-secret-key" --project SH.API
dotnet user-secrets set "Stripe:ApiKey" "your-stripe-secret-key" --project SH.API
```

## Configuration Reference

| Section | Property | Purpose |
|---|---|---|
| `CosmosDB` | `DatabaseId` | Azure Cosmos DB database identifier used by the repositories. |
| `CosmosDB` | `ConnectionString` | Azure Cosmos DB connection string. |
| `Azure` | `BlobConnectionString` | Azure Storage connection string used for property and room photo storage. |
| `Azure` | `CognitiveKey` | Azure Cognitive Services key. |
| `Azure` | `CognitiveEndpoint` | Azure Cognitive Services endpoint. |
| `Apple` | `Endpoint` | Apple receipt verification endpoint. Use the production or sandbox endpoint appropriate to the receipt environment. |
| `Apple` | `SharedSecret` | App Store shared secret used for receipt verification. Treat as confidential. |
| `Apple` | `BundleId` | Apple application bundle identifier. |
| `Apple:Premium` | `PriceMonth` | Monthly App Store product identifier. |
| `Apple:Premium` | `PriceYear` | Yearly App Store product identifier. |
| `Stripe` | `ApiKey` | Stripe secret API key. Treat as confidential. |
| `Stripe` | `SigningSecret` | Stripe webhook signing secret. Treat as confidential. |
| `Stripe:Premium` | `PriceMonth` | Monthly Stripe Price ID. |
| `Stripe:Premium` | `PriceYear` | Yearly Stripe Price ID. |
| `Here` | `ApiKey` | HERE API key used by location and geocoding functionality. |
| `Didit` | `ApiKey` | Didit API key for identity verification integration. |
| `Didit` | `WorkflowId` | Didit workflow identifier. |
| `Didit` | `SecretKey` | Didit secret key. Treat as confidential. |
| `ClerkAuth` | `SecretKey` | Clerk backend secret key. Treat as confidential. |
| `ClerkAuth` | `SigningSecret` | Clerk webhook signing secret. Treat as confidential. |
| `UserJot` | `ProjectId` | UserJot project identifier. |
| `UserJot` | `Secret` | UserJot API secret. Treat as confidential. |
| `ZeptoMail` | `ApiKey` | ZeptoMail API key used for transactional email delivery. Treat as confidential. |
| Root | `IsMaintenanceMode` | Enables or disables API maintenance mode. |

The configuration class defines all properties listed above. Some integrations are only needed when their corresponding features are used.

## local.settings.json

Azure Functions Core Tools uses `local.settings.json` for local runtime settings. The repository includes a base file with the Functions runtime settings:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
  },
  "Host": {
    "CORS": "*"
  }
}
```

The API also loads this file directly as application configuration. If you prefer to keep local configuration in this file instead of User Secrets, add the application sections shown in the example above as root-level properties, alongside `Values` and `Host`. Do not put those sections inside `Values`, because the application binds configuration sections from the root.

For local Azure Storage emulation, start [Azurite](https://learn.microsoft.com/azure/storage/common/storage-use-azurite) when using `UseDevelopmentStorage=true`.

## Frontend Configuration

When running the frontend locally, `SH.WEB` requires the `ApiBaseAddress` configuration value to point to the local API base URL. The frontend reads this value when its base address is localhost or 127.0.0.1.

For deployed environments, the frontend derives the API URL from its own base address and uses the `api/` path.

## Azure Deployment

For deployed environments, provide backend configuration values through the Azure Function App application settings or the configuration provider used by your deployment process.

Use hierarchical keys with double underscores, for example:

```text
CosmosDB__DatabaseId
CosmosDB__ConnectionString
Azure__BlobConnectionString
ClerkAuth__SecretKey
Stripe__ApiKey
```

Never deploy development User Secrets or commit credentials to source control.
