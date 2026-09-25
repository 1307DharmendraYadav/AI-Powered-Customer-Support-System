🤖 AI-Powered Customer Support System
=====================================

> A production-inspired ASP.NET Core 10 Web API built with Clean Architecture, Entity Framework Core, SQL Server, and JWT Authentication — progressively enhanced with Generative AI, RAG, Tool Calling, AI Agents, and MCP.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10.0-512BD4?style=for-the-badge&logo=dotnet)
![EF Core](https://img.shields.io/badge/EF_Core-10.0-68217A?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-Clean-6DB33F?style=for-the-badge)
![AI](https://img.shields.io/badge/AI-Generative%20%2B%20RAG%20%2B%20Agents-FF6F00?style=for-the-badge&logo=openai&logoColor=white)
![Security](https://img.shields.io/badge/Security-JWT-black?style=for-the-badge&logo=jsonwebtokens)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

---

## 🚧 Current Status

This project is being built chapter-by-chapter as part of the **AI for .NET Developers** training program, with each chapter tracked on its own Git branch so the commit/branch history mirrors the learning progression.

| Chapter | Branch | Topic | Status |
|---|---|---|---|
| 1.1 | `main` | Project Overview, Users, and Application Architecture | ✅ Done |
| 1.2 | `chapter-1-2-solution-efcore-sqlserver` | Solution Creation, SQL Server DB Design, EF Core, Entities & Migrations | ✅ Done |
| 1.3 | `chapter-1-3-dtos-validation-mapping` | DTOs, Data Annotation Validation, Manual Mapping | ✅ Done |
| 1.4 | `chapter-1-4-exceptions-global-handling` | Custom Exceptions and Global Exception Handling | ✅ Done |
| 1.5 | `chapter-1-5-repositories` | Creating Repositories (Interfaces + EF Core Implementations) | ✅ Done |
| 1.6 | `chapter-1-6-services` | Creating Services (Application Service Interfaces + Implementations) | ✅ Done |
| 1.7 | `chapter-1-7-jwt-authentication` | JWT Authentication with Access Token and Refresh Token | ✅ Done |
| 1.8 | — | Authorization and Current User Context | ⏳ Upcoming |
| 1.9 | — | Product, Ticket Category, and Master Data APIs | ⏳ Upcoming |
| 1.10 | — | Support Ticket Creation and Retrieval | ⏳ Upcoming |

---

## 🏗️ Architecture

Built using **Clean Architecture** with clear separation of concerns:

```
CustomerSupportSystem/
├── CustomerSupport.Domain/           # Entities, Enums, core business rules
├── CustomerSupport.Application/      # DTOs, Mapping Extensions, Use Cases, Interfaces
├── CustomerSupport.Infrastructure/    # EF Core, DbContext, Migrations, Repositories
└── CustomerSupport.API/              # ASP.NET Core Web API, Controllers, Program.cs
```

---

## 📦 Domain Model

| Entity | Purpose |
|---|---|
| `User` / `Role` | User accounts and role-based access |
| `SupportTicket` | Core support request record |
| `TicketAssignment` | Assigning tickets to support executives |
| `TicketComment` / `TicketHistory` | Ticket communication and audit trail |
| `TicketAttachment` | File uploads on tickets |
| `TicketCategory` / `TicketPriority` / `TicketStatus` | Ticket classification |
| `FAQ` / `KnowledgeBaseArticle` | Self-service knowledge content (future RAG source) |
| `RefreshToken` | JWT refresh token storage |
| `Product` | Products linked to support requests |

## 📨 DTOs & Mapping (Chapter 1.3)

To keep a secure boundary between API clients and the Domain model, this project uses dedicated **Request/Response DTOs** with **Data Annotation validation**, and **manual mapping** via extension methods (no AutoMapper) so every property copy is explicit and controlled.

- `Auth`: `RegisterRequestDTO`, `LoginRequestDTO`, `RefreshTokenRequestDTO`, `UserResponseDTO`
- `Products`: `CreateProductRequestDTO`, `UpdateProductRequestDTO`, `ProductResponseDTO`
- `Categories`: `CreateTicketCategoryRequestDTO`, `UpdateTicketCategoryRequestDTO`, `TicketCategoryResponseDTO`
- `Tickets`: `CreateTicketRequestDTO`, `UpdateTicketRequestDTO`, `AssignTicketRequestDTO`, `ChangeTicketStatusRequestDTO`, `AddTicketCommentRequestDTO`, `TicketSummaryResponseDTO`, `TicketDetailsResponseDTO`, `TicketCommentResponseDTO`, `TicketHistoryResponseDTO`
- `MasterData`: `TicketPriorityResponseDTO`, `TicketStatusResponseDTO`
- Mapping extensions in `CustomerSupport.Application/Mappings/` for `User`, `Product`, `Ticket`, `TicketCategory`, `TicketPriority`, `TicketStatus`

## ⚠️ Exception Handling (Chapter 1.4)

Instead of repetitive try-catch blocks scattered across Controllers and Services, this project uses a small set of **custom exception types** in the Application layer, caught centrally by a **Global Exception Handler** in the API layer and converted into a consistent `ApiResponse<T>` response.

- `CustomerSupport.Application/Exceptions/`
  - `NotFoundException` — requested resource doesn't exist (Product, Ticket, Category, User, KB Article)
  - `ConflictException` — e.g. duplicate email/registration conflicts
  - `BusinessRuleException` — violates an application business rule (e.g. invalid ticket status transition)
  - `ApplicationValidationException` — application-level validation failures
- `CustomerSupport.API/Models/ApiResponse.cs` — consistent success/error response envelope returned to all clients
- `CustomerSupport.API/ExceptionHandling/GlobalExceptionHandler.cs` — catches exceptions, logs them, and maps each type to the correct HTTP status code
- `CustomerSupport.API/Extensions/ExceptionHandlingExtensions.cs` — registers the handler via `AddGlobalExceptionHandling()`

This keeps Controllers and Services clean, centralizes error handling in one place, and guarantees clients always receive a predictable error-response shape.


## 🗄️ Repositories (Chapter 1.5)

The Application layer is connected to SQL Server through the **Repository pattern**, while remaining fully independent of Entity Framework Core — the Application layer defines *what* data operations are needed; the Infrastructure layer defines *how* they're performed.

- `CustomerSupport.Application/Interfaces/Repositories/`
  - `IUserRepository`, `IRefreshTokenRepository`, `IProductRepository`,
    `ITicketCategoryRepository`, `ITicketPriorityRepository`,
    `ITicketStatusRepository`, `ITicketRepository`
- `CustomerSupport.Infrastructure/Repositories/`
  - EF Core implementations of all interfaces above, using `CustomerSupportDbContext`
  - Read-only queries use `AsNoTracking()`; update-capable reads support
    optional change tracking via a `trackChanges` parameter
  - `TicketRepository` eager-loads related Customer, Product, Category,
    Priority, Status, AssignedToUser, Comments, and History for a complete
    Ticket representation

Ticket Priority and Ticket Status repositories are **read-only**, since those values are controlled by predefined Enums (`TicketPriorityType`, `TicketStatusType`) rather than free-form admin-managed master data like Ticket Categories.



## ⚙️ Application Services (Chapter 1.6)

Application Services sit between the (upcoming) Controllers and the Repository layer, coordinating business logic, DTO mapping, and validation before touching the database.

- `CustomerSupport.Application/Interfaces/Services/`
  - `IMasterDataService` — read-only access to Ticket Priority and Ticket Status master data
  - `IProductService` — Product retrieval and creation, coordinating `IProductRepository`, DTO mapping, and uniqueness validation
  - `ITicketCategoryService` — Ticket Category retrieval and creation, coordinating `ITicketCategoryRepository`, DTO mapping, and uniqueness validation
- `CustomerSupport.Application/Services/`
  - `MasterDataService`, `ProductService`, `TicketCategoryService` — implementations using the DTOs (Ch. 1.3), custom exceptions (Ch. 1.4), and repositories (Ch. 1.5) built in earlier chapters
- `CustomerSupport.Application/Extensions/ApplicationServiceExtensions.cs`
  - `AddApplicationServices()` — registers all Application Services with the DI container, wired up in `Program.cs`

Also completed in this chapter: full DI registration of all 7 repository interfaces (`IUserRepository`, `IRefreshTokenRepository`, `IProductRepository`, `ITicketCategoryRepository`, `ITicketPriorityRepository`, `ITicketStatusRepository`, `ITicketRepository`) in `InfrastructureServiceExtensions.cs`, completing the wiring that Chapter 1.5 introduced.


## 🔐 JWT Authentication (Chapter 1.7)

Implements stateless authentication using signed JWTs, with password hashing and refresh token support.

- `CustomerSupport.Application/Interfaces/Authentication/`
  - `IPasswordService` — password hashing and verification abstraction
  - `ITokenService` — access/refresh token generation abstraction
- `CustomerSupport.Application/Services/AuthService.cs` — orchestrates the full authentication workflow (registration, login, token generation, refresh token rotation, revocation) using only abstractions, independent of any specific hashing or JWT library
- `CustomerSupport.Infrastructure/Authentication/`
  - `PasswordService` — implements `IPasswordService` using ASP.NET Core Identity's `PasswordHasher<User>`
  - `JwtSettings` / `JwtTokenService` — signed JWT generation and validation configuration
- `CustomerSupport.API/Extensions/AuthenticationExtensions.cs` — `AddJwtAuthentication()`, wiring JWT Bearer authentication into the pipeline
- `CustomerSupport.API/Controllers/AuthController.cs` — the API's first public endpoints (Register, Login, Refresh Token, Logout)
- `UnauthorizedException` — new exception type handled by the existing Global Exception Handler (Ch. 1.4)

**Security note:** The SQL Server connection string and JWT signing key have been moved out of `appsettings.json` into the gitignored `appsettings.Development.json`, so no secrets are committed to source control going forward.

---

## 🛠️ Tech Stack

- **.NET 10** / **ASP.NET Core Web API**
- **Entity Framework Core** (Code-First)
- **SQL Server**
- **Clean Architecture** (Domain-Application-Infrastructure-API)
- **Swagger / OpenAPI**
- - **JWT Authentication & Refresh Tokens** (implemented — see Chapter 1.7 above)
- **DTOs + Data Annotation Validation + Manual Mapping**

**Coming soon:**
- OpenAI / Azure OpenAI / Microsoft Foundry
- Microsoft.Extensions.AI, Microsoft Agent Framework
- Azure AI Search (Vector + Hybrid Search)
- Model Context Protocol (MCP)
- Docker, Azure Container Apps, Azure DevOps Pipelines

---

## 🚀 Getting Started

```bash
git clone https://github.com/1307DharmendraYadav/AI-Powered-Customer-Support-System.git
cd AI-Powered-Customer-Support-System
dotnet restore
dotnet build
```

Update your SQL Server connection string in `CustomerSupport.API/appsettings.Development.json`, then run migrations:

```bash
dotnet ef database update --project CustomerSupport.Infrastructure --startup-project CustomerSupport.API
```

Run the API:

```bash
dotnet run --project CustomerSupport.API
```

---

## 🌿 Branching Strategy

Each course chapter is developed on its own branch, following the convention:

```
chapter-<module>-<chapter>-<short-topic>
```

Branches are merged into `main` once a chapter is stable and reviewed, keeping `main` as the latest working checkpoint while preserving full chapter-by-chapter history for anyone browsing the repo.

---

## 📄 License

This project is licensed under the MIT License.

---

## 🙋 About

This project is being developed as part of the **AI for .NET Developers** training program by [DotNetTutorials](https://dotnettutorials.net), demonstrating the journey from a traditional enterprise application to a fully AI-powered production system.
