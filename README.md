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

This project is being built in stages, starting with a solid traditional business application before layering in AI capabilities.

- ✅ **Stage 1 — Core Business Application**: Clean Architecture (Domain, Application, Infrastructure, API), EF Core Code-First, SQL Server, ticketing domain model
- 🔄 **Stage 2 — Authentication & Authorization**: JWT, Refresh Tokens, Role-Based Access Control *(in progress)*
- ⏳ **Stage 3 — Generative AI Integration**: Content generation, structured outputs, conversational assistant
- ⏳ **Stage 4 — RAG Knowledge Assistant**: Semantic search over FAQs & Knowledge Base Articles
- ⏳ **Stage 5 — Tool Calling & AI Agents**: AI-driven ticket triage, assignment, and automation
- ⏳ **Stage 6 — Deployment**: Docker, Azure Container Apps, Azure DevOps CI/CD

---

## 🏗️ Architecture

Built using **Clean Architecture** with clear separation of concerns:

```
CustomerSupportSystem/
├── CustomerSupport.Domain/           # Entities, Enums, core business rules
├── CustomerSupport.Application/      # Use cases, DTOs, interfaces
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

---

## 🛠️ Tech Stack

- **.NET 10** / **ASP.NET Core Web API**
- **Entity Framework Core** (Code-First)
- **SQL Server**
- **Clean Architecture** (Domain-Application-Infrastructure-API)
- **Swagger / OpenAPI**
- **JWT Authentication & Refresh Tokens**

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

## 📄 License

This project is licensed under the MIT License.

---

## 🙋 About

This project is being developed as part of the **AI for .NET Developers** training program by [DotNetTutorials](https://dotnettutorials.net), demonstrating the journey from a traditional enterprise application to a fully AI-powered production system.
