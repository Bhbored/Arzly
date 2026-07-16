# Arzly

A classified advertisements platform built with **.NET 10**, **ASP.NET Core Web API**, and **Clean Architecture**. Arzly connects buyers and sellers across 11+ categories with a comprehensive listing management system, real-time chat, support ticketing, and location-based search.

---

## Tech Stack

| Category | Technology |
|----------|-----------|
| **Runtime** | .NET 10.0 (`net10.0`) |
| **Framework** | ASP.NET Core Web API |
| **ORM** | Entity Framework Core 10.0.10 |
| **Database** | SQL Server (LocalDB) |
| **Authentication** | ASP.NET Core Identity + JWT Bearer + Google OAuth + Firebase |
| **API Versioning** | Asp.Versioning.Mvc 10.0.0 |
| **Swagger** | Swashbuckle 10.2.3 |
| **Logging** | Serilog + Seq |
| **File Storage** | Cloudflare R2 (S3-compatible, via AWSSDK.S3) |
| **Email** | MailKit (SMTP Gmail) |
| **Geocoding** | Google Maps API (Places, Geocoding, Static Maps) |
| **Testing** | xUnit 2.9.3 |

---

## Solution Structure

```
Arzly.slnx
├── Arzly.Api/           ASP.NET Core Web API (http://localhost:5215)
├── Arzly.Shared/        Class library — DTOs, enums, constants, extensions
└── integrationTESTS/    xUnit test project (stub)
```

### Project Map

#### Arzly.Api — The Main API Backend

| Layer | Path | Responsibility |
|-------|------|---------------|
| **Presentation** | `Controllers/` | HTTP routing, request validation, response formatting |
| **Application** | `Application/Contracts/` | Service interfaces (use-case contracts) |
| **Application** | `Application/Services/` | Business logic and use-case implementations |
| **Domain** | `Domain/Entities/` | EF Core entity models |
| **Domain** | `Domain/Contracts/` | Repository interfaces (data-access contracts) |
| **Infrastructure** | `Infrastructure/Data/` | `AppDbContext`, EF configurations, seed data |
| **Infrastructure** | `Infrastructure/Identity/` | `ApplicationUser`, `ApplicationRole` (Guid keys) |
| **Infrastructure** | `Infrastructure/Repositories/` | EF Core repository implementations |
| **Infrastructure** | `Infrastructure/Storage/` | Cloudflare R2 image uploader |
| **Infrastructure** | `Migrations/` | EF Core migrations (7 sets) |
| **Infrastructure** | `Mappings/` | Manual Entity ↔ DTO mappings (no AutoMapper) |
| **Infrastructure** | `Helpers/` | DI registration, listing filters, Google Maps integration |
| **Infrastructure** | `Filters/` | Exception, result, and action filters |
| **Infrastructure** | `Hubs/` | Email service (MailKit Gmail SMTP) |

#### Arzly.Shared — Cross-Cutting Library

| Path | Contents |
|------|----------|
| `DTOs/Request/` | 19 request DTO subdirectories (Auth, Category, Chat, Listing, etc.) |
| `DTOs/Response/` | 18 response DTO subdirectories |
| `Enums/` | Extensive enum library across 11 listing categories + jobs, notifications, tickets, user preferences |
| `Constants/` | Standard error message strings |
| `Extensions/` | `ClaimsPrincipalExtensions` (userId extraction) |

---

## Architecture

### Clean Architecture Layering

```
┌──────────────────────────────────────────────┐
│            Controllers (API)                 │
│  Auth, Categories, Listings, Communications, │
│  Locations, Support, Upload, Users, Admin    │
├──────────────────────────────────────────────┤
│           Application Layer (Services)       │
│  Business logic, orchestration, use cases    │
├──────────────────────────────────────────────┤
│             Domain Layer (Entities)          │
│  EF Core models + Repository interfaces      │
├──────────────────────────────────────────────┤
│          Infrastructure Layer                │
│  ┌──────────┐ ┌──────────┐ ┌──────────────┐ │
│  │ Data     │ │ Identity │ │ Storage (R2) │ │
│  │ DbContext│ │ AppUser  │ │ ImageUploader│ │
│  │ Configs  │ │ AppRole  │ └──────────────┘ │
│  │ Seed     │ └──────────┘ ┌──────────────┐ │
│  │ Migrate  │              │ Repositories │ │
│  └──────────┘              │ (EF Core)    │ │
│   Google Maps   Email      └──────────────┘ │
├──────────────────────────────────────────────┤
│          Shared Layer (DTOs/Enums)           │
└──────────────────────────────────────────────┘
```

### Key Design Patterns

- **Clean Architecture**: Separation of concerns with dependency inversion throughout
- **Generic Repository**: `BaseRepository<TEntity, TKey>` base class for all repositories
- **Generic Service**: `BaseService<TEntity, TDto, TCreateDto, TUpdateDto, TKey>` abstract base
- **Manual Mapping**: 16 explicit mapper classes — no AutoMapper dependency
- **Discriminated Listings**: 11 listing-detail types, each with its own table, EF config, filter, and repository
- **Global Authorization**: All controllers require authentication by default via `AuthorizeFilter`
- **API Versioning**: URL segment reader (`arzly/v1/controller`), default v1.0

---

## Database Schema

### Phase 1 — Core (Active)

| Entity | Table | Description |
|--------|-------|-------------|
| `ApplicationUser` | `AspNetUsers` | Extended IdentityUser with AuthMethod, RefreshToken, ban support |
| `ApplicationRole` | `AspNetRoles` | Standard IdentityRole\<Guid\> |
| `Category` | `Categories` | 11 seed categories (Vehicles, Real Estate, Phones & Gadgets, etc.) |
| `SubCategory` | `SubCategories` | Category sub-groups |
| `Listing` | `Listings` | Main listing entity |
| `JobListing` | `JobListings` | Job-specific listings |
| `PickupLocation` | `PickupLocations` | Saved delivery locations |
| `SavedListing` | `SavedListings` | User-favorited listings (composite key) |
| `SearchQuery` | `SearchQueries` | Saved search history |
| `UserProfile` | `UserProfiles` | Extended user profile data |
| `UserReport` | `UserReports` | User moderation reports |
| `UserActivityLog` | `UserActivityLogs` | Audit trail |
| `UserPreference` | `UserPreferences` | Theme, language preferences |
| `BabyChildDetails`–`VehiclesDetails` | 11 detail tables | Discriminated listing details |

### Phase 2 — Communications & Support (Planned)

| Entity | Table | Description |
|--------|-------|-------------|
| `Chat` | `Chats` | Conversations (with listing context) |
| `ChatMessage` | `ChatMessages` | Individual messages (IsRead tracking) |
| `Ticket` | `Tickets` | Support tickets |
| `TicketAttachment` | `TicketAttachments` | Support file attachments |
| `TicketMessage` | `TicketMessages` | Support ticket messages |
| `Notification` | `Notifications` | User notifications and broadcasts |

---

## API Overview

Base URL: `http://localhost:5215/arzly/v{version:apiVersion}/[controller]`

Authentication is required on all endpoints by default.

### Controller Map

| Area | Controller | Description |
|------|-----------|-------------|
| Auth | `Authentication` | Login, Register, RefreshToken, Firebase auth |
| Auth | `Email` | Email verification, password reset |
| Categories | `Category` | Category CRUD |
| Categories | `SubCategory` | SubCategory CRUD |
| Communications | `Chat` | Chat and messaging |
| Listings | `Listing` | Listing CRUD, browse, search |
| Listings | `JobListing` | Job listing CRUD |
| Listings | `SavedListing` | Save/unsave listings |
| Listings | `SearchQuery` | Saved search queries |
| Locations | `Location` | Location management |
| Locations | `PickupLocation` | User pickup locations |
| Support | `UserReport` | Report users |
| Upload | `Upload` | Image/file upload |
| Users | `UserProfile` | User profile CRUD |
| Admin | `ListingAdmin` | Listing moderation |

### Auth Flow

1. **Register** via email/password or Firebase
2. **Login** returns short-lived JWT (1 min) + refresh token (7 days)
3. **RefreshToken** endpoint issues new JWT without re-authentication
4. JWT is sent via `Authorization: Bearer <token>` header

---

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (comes with Visual Studio)
- [Node.js](https://nodejs.org/) (for client-side tooling, if applicable)

### Setup

```powershell
# Clone the repository
git clone <repo-url>
cd Arzly

# Restore dependencies
dotnet restore

# Update the database (applies migrations)
dotnet ef database update --project Arzly.Api

# Run the API
dotnet run --project Arzly.Api
```

Swagger UI will be available at `http://localhost:5215/swagger`.

### Configuration

Key settings in `Arzly.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Arzly;..."
  },
  "AllowedOrigins": ["https://localhost:7251"],
  "JwtSettings": {
    "Issuer": "...",
    "Audience": "...",
    "EXPIRATION_MINUTES": 1,
    "Key": "..."
  },
  "RefreshToken": {
    "EXPIRATION_DAYS": 7
  },
  "CloudflareR2": {
    "AccessKey": "...",
    "SecretKey": "...",
    "ServiceURL": "...",
    "BucketName": "...",
    "PublicUrlBase": "..."
  },
  "GoogleMaps": {
    "ApiKey": "..."
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "SenderEmail": "...",
    "Password": "..."
  }
}
```

> **⚠️ Security**: Development secrets are currently in `appsettings.Development.json`. Move sensitive values to environment variables, user secrets, or a secrets manager before deploying.

---

## Commands

```powershell
# Build the solution
dotnet build

# Run API
dotnet run --project Arzly.Api

# Add EF Core migration
dotnet ef migrations add <MigrationName> --project Arzly.Api

# Apply migrations to database
dotnet ef database update --project Arzly.Api

# Run tests
dotnet test
```

---

## Project Status

- **Phase 1** ✅ — Core classifieds functionality (categories, listings, auth, users)
- **Phase 2** 🚧 — Chat, support tickets, notifications (tables exist, services in progress)
- **Client** ❌ — No frontend exists yet (future Blazor or other SPA)
- **Tests** ❌ — Test project is a stub with no actual tests
- **CI/CD** ❌ — No workflows configured

### Known Security Items

- JWT secret, API keys, and SMTP credentials are committed in `appsettings.Development.json`
- JWT expiration is set to 1 minute (acceptable for dev with refresh-token flow)

---

## Infrastructure Dependencies (External Services)

| Service | Purpose | Required for dev? |
|---------|---------|-------------------|
| SQL Server LocalDB | Database | ✅ Yes |
| Seq | Log aggregation | ❌ Optional (logs fall back to console) |
| Cloudflare R2 | Image/file storage | ❌ Optional (upload endpoints fail without it) |
| Google Maps API | Geocoding, autocomplete | ❌ Optional (location features fail) |
| Google OAuth | Social login | ❌ Optional |
| Gmail SMTP | Email sending | ❌ Optional |
| Firebase | Alternative auth provider | ❌ Optional |
