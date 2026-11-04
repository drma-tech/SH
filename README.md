# Shared Home

[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fdrma-tech%2FSH.svg?type=shield&issueType=license)](https://app.fossa.com/projects/git%2Bgithub.com%2FSH?ref=badge_shield&issueType=license)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fdrma-tech%2FSH.svg?type=shield&issueType=security)](https://app.fossa.com/projects/git%2Bgithub.com%2Fdrma-tech%2FSH?ref=badge_shield&issueType=security)
[![Quality gate status](https://sonarcloud.io/api/project_badges/measure?project=drma-tech_SH&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=drma-tech_SH)

**Find a room, discover the people you may live with, and make shared living easier to arrange.**

[Shared Home](https://shared-home.com) is a global shared-housing discovery platform that connects people offering rooms in occupied properties with people looking for a place to live.

Shared Home is focused on rooms in shared homes, not entire apartments or properties. A listed property must already have at least one resident, such as the host or another tenant. The platform helps people discover rooms and learn about the people they may share a home with.

Shared Home is a discovery and advertising platform, not a rental agency or payment intermediary. Hosts and prospective residents arrange rental terms, agreements, and payments directly with each other.

## Features

### Room Marketplace

- Create and manage property information.
- Add rooms with descriptions, photos, size, access type, facilities, and rental details.
- Specify rental costs, payment frequency, currency, and stay requirements.
- Manage property amenities, accessibility details, and available facilities.

### Rental Proposals

- Submit and manage proposals for available rooms.
- Track proposal statuses, including pending, approved, rejected, and cancelled.
- Manage property members and their roles.

### User Profiles

- Create profiles with personal details, languages, nationality, occupation, and lifestyle information.
- Add verification details for identity, email, and phone.
- Share information that can help hosts and residents assess whether a shared-living arrangement may be suitable.

### Shared-Living Discovery

Shared Home is designed to help people discover both rooms and the people who live in the properties offering them. Room listings, user profiles, and shared-living preferences provide the foundation for this experience.

Guest-side room advertising, complete public discovery of room-seeker profiles, automated roommate compatibility matching, and the private resident community experience are planned areas of development and are not currently available as complete features.

## Technology Stack

| Area | Technology |
|---|---|
| Frontend | Blazor WebAssembly |
| Backend | Azure Functions, .NET 10 |
| Language | C# |
| UI | MudBlazor |
| Database | Azure Cosmos DB |
| File Storage | Azure Blob Storage |
| Authentication | Clerk |
| Payments | Stripe |
| Geocoding and Maps | HERE |
| Resilience | Polly |
| Monitoring | Sentry |
| Analytics | Google Analytics |

## Architecture

The solution is organized into three main projects:

| Project | Description |
|---|---|
| `SH.WEB` | Blazor WebAssembly frontend |
| `SH.API` | Azure Functions backend |
| `SH.Shared` | Shared models, validation, translations, and contracts |

The frontend communicates with the backend API for application data and operations. The backend integrates with Azure services and external providers for persistence, file storage, authentication, payments, and location-related functionality.

## External Services

Shared Home integrates with external services for application functionality and operations:

- **Clerk:** Authentication and user identity management.
- **Stripe:** Payment processing.
- **HERE:** Geocoding and location-related services.
- **Didit:** Identity verification integration.
- **Sentry:** Error monitoring and diagnostics.
- **Google Analytics:** Usage analytics.
- **Google AdSense:** Advertising.

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Azure Functions Core Tools](https://learn.microsoft.com/azure/azure-functions/functions-run-local)
- An Azure Cosmos DB account
- An Azure Storage account
- Credentials for the external services required by the application

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/drma-tech/SH.git
   ```

2. Open `SH.sln` in Visual Studio or your preferred .NET IDE.

3. Configure the backend settings in `SH.API/local.settings.json` or use .NET User Secrets for local development. See the [Configuration Guide](docs/CONFIGURATION.md) for details.

4. Start the backend from the `SH.API` directory:

   ```bash
   func start
   ```

5. Configure the frontend API base address and run the `SH.WEB` project.

Some features depend on external services and valid credentials. A complete local environment requires the corresponding service accounts and configuration.

## Documentation

- [Configuration Guide](docs/CONFIGURATION.md)
- [Roadmap](ROADMAP.md)

## License

See the repository's license file for licensing information.
