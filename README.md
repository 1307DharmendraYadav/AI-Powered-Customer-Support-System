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
| 1.4 | — | AI Development Environment & Secure Provider Setup | ⏳ Upcoming |
| 1.5 | — | Prompt Engineering & Context Engineering | ⏳ Upcoming |
| 2.x | — | Generative AI, Structured Outputs, Tool Calling, Streaming | ⏳ Upcoming |
| 4.x | — | Embeddings, Vector Search, RAG | ⏳ Upcoming |
| 5.x | — | AI Agents, Microsoft Agent Framework, MCP | ⏳ Upcoming |
| 10.x | — | Docker, Azure Deployment, CI/CD | ⏳ Upcoming |

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

---

## 🛠️ Tech Stack

- **.NET 10** / **ASP.NET Core Web API**
- **Entity Framework Core** (Code-First)
- **SQL Server**
- **Clean Architecture** (Domain-Application-Infrastructure-API)
- **Swagger / OpenAPI**
- **JWT Authentication & Refresh Tokens**
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
