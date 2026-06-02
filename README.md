# PawClinic

A veterinary clinic management system built with **Clean Architecture**, **ASP.NET Core Minimal APIs**, and **Blazor WebAssembly**.

## Overview

PawClinic lets clinic staff and pet owners manage appointments, pets, owners, and veterinarians through a secure REST API backed by a Blazor WebAssembly front-end. Authentication is handled with JWT bearer tokens issued via ASP.NET Core Identity.

## Architecture

The solution follows Clean Architecture principles and is divided into the following projects:

| Project | Role |
|---|---|
| `PawClinic.Api` | ASP.NET Core Minimal API host — maps endpoints, registers middleware, wires up DI |
| `PawClinic.App` | Blazor WebAssembly front-end |
| `PawClinic.Application` | Application layer — CQRS handlers (MediatR), AutoMapper profiles, FluentValidation validators |
| `PawClinic.Domain` | Domain entities, enums, and base types |
| `PawClinic.Identity` | JWT authentication service built on ASP.NET Core Identity |
| `PawClinic.Infrastructure` | Cross-cutting services (e-mail via SendGrid) |
| `PawClinic.Persistence` | EF Core DbContext, migrations, and repository implementations |
| `PawClinic.Application.UnitTests` | Unit tests for application-layer handlers |
| `PawClinic.Api.IntegrationTests` | Integration tests for the API endpoints |
| `PawClinic.Persistence.IntegrationTests` | Integration tests for the persistence layer |

### Dependency flow

```
Api  ──►  Application  ──►  Domain
          ▲
Identity ─┤
Persistence─┤
Infrastructure─┘
```

## Technologies

- **.NET 10** — target framework for all projects
- **ASP.NET Core Minimal APIs** — lightweight endpoint routing
- **Blazor WebAssembly** — browser-side front-end
- **MediatR** — CQRS command/query dispatching
- **AutoMapper** — object mapping between layers
- **FluentValidation** — request validation
- **Entity Framework Core 10** — ORM with SQLite (development) and SQL Server support
- **ASP.NET Core Identity + JWT** — authentication and authorisation
- **Serilog** — structured logging to the console
- **SendGrid** — transactional e-mail
- **OpenAPI** — auto-generated API documentation (available in Development)

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Getting started

### 1. Clone the repository

```bash
git clone https://github.com/GillCleeren/CA-minimal-api.git
cd CA-minimal-api
```

### 2. Configure settings

Open `PawClinic.Api/appsettings.json` and update the values that need to be changed for your environment:

```json
{
  "ConnectionStrings": {
    "PawClinicConnectionString": "Data Source=pawclinic.db",
    "PawClinicIdentityConnectionString": "Data Source=pawclinicidentity.db"
  },
  "JwtSettings": {
    "Key": "<your-secret-key-min-32-chars>",
    "Issuer": "PawClinicApi",
    "Audience": "PawClinicClient",
    "DurationInMinutes": 60
  },
  "EmailSettings": {
    "ApiKey": "<your-sendgrid-api-key>",
    "FromAddress": "noreply@pawclinic.com",
    "FromName": "PawClinic"
  }
}
```

> **Note:** On every startup the database is deleted and re-created from migrations so the application always starts with a clean state. Remove the `ResetDatabaseAsync` call in `Program.cs` for production use.

### 3. Run the API

```bash
cd PawClinic.Api
dotnet run
```

The API is available at `https://localhost:7xxx` (port shown in the console). When running in Development, the OpenAPI document is served at `/openapi/v1.json`.

### 4. Run the Blazor front-end

```bash
cd PawClinic.App
dotnet run
```

### 5. Run the tests

```bash
dotnet test
```

## API endpoints

All endpoints except `/api/account/*` require a valid JWT bearer token.  
Include the token in the `Authorization` header: `Authorization: ******

### Account

| Method | Path | Description | Auth required |
|--------|------|-------------|:---:|
| `POST` | `/api/account/register` | Register a new user | ✗ |
| `POST` | `/api/account/authenticate` | Obtain a JWT token | ✗ |

### Owners

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/owners?page=1&size=10` | List all owners (paginated) |
| `GET` | `/api/owners/{id}` | Get owner by ID |
| `POST` | `/api/owners` | Register a new owner |
| `PUT` | `/api/owners/{id}/contact` | Update owner contact details |

### Pets

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/pets/{id}` | Get pet by ID |
| `GET` | `/api/pets/owner/{ownerId}` | List pets for an owner |
| `POST` | `/api/pets` | Add a new pet |
| `PUT` | `/api/pets/{id}/archive` | Archive a pet |

### Appointments

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/appointments/{id}` | Get appointment by ID |
| `GET` | `/api/appointments/upcoming?vetId=&petId=` | List upcoming appointments |
| `GET` | `/api/appointments/history/{petId}` | Get appointment history for a pet |
| `POST` | `/api/appointments` | Schedule a new appointment |
| `PUT` | `/api/appointments/{id}/cancel` | Cancel an appointment |
| `PUT` | `/api/appointments/{id}/complete` | Mark an appointment as complete |

### Vets

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/vets` | List all vets |
| `GET` | `/api/vets/{id}/schedule?date=YYYY-MM-DD` | Get a vet's schedule for a date |

## Project structure

```
CA-minimal-api/
├── PawClinic.Api/                  # Minimal API host
│   ├── Endpoints/                  # Endpoint mapping (one file per resource)
│   ├── Middleware/                 # Custom exception handler
│   ├── Services/                   # LoggedInUserService
│   ├── Program.cs
│   └── StartupExtensions.cs
├── PawClinic.App/                  # Blazor WebAssembly front-end
├── PawClinic.Application/          # CQRS handlers, validators, AutoMapper profiles
│   └── Features/
│       ├── Appointments/
│       ├── Owners/
│       ├── Pets/
│       └── Vets/
├── PawClinic.Domain/               # Entities, enums, base types
├── PawClinic.Identity/             # JWT + ASP.NET Identity
├── PawClinic.Infrastructure/       # Email (SendGrid)
├── PawClinic.Persistence/          # EF Core, migrations, repositories
├── PawClinic.Application.UnitTests/
├── PawClinic.Api.IntegrationTests/
├── PawClinic.Persistence.IntegrationTests/
└── PawClinic.slnx
```
