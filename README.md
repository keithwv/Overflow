# Overflow

Overflow is a distributed microservices application built with .NET 10 and [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview). It provides a platform for asking and searching questions, featuring a modern architecture with messaging, full-text search, and centralized authentication.

## 🚀 Stack & Technologies

- **Language:** C# 14.0 (.NET 10.0)
- **Orchestration:** .NET Aspire
- **Microservices:**
  - **QuestionService:** Manages questions and tags (ASP.NET Core MVC/Controllers, EF Core, PostgreSQL).
  - **SearchService:** Provides search capabilities (Typesense integration).
- **Infrastructure:**
  - **Database:** PostgreSQL (Question database).
  - **Search Engine:** Typesense.
  - **Messaging:** RabbitMQ with [Wolverine](https://wolverine.netlify.app/).
  - **Identity Provider:** Keycloak.
  - **Gateway:** YARP (Yet Another Reverse Proxy).
  - **Observability:** OpenTelemetry & Aspire Dashboard.

## 📋 Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for Aspire resources and infrastructure)
- [.NET Aspire workload](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/setup-tooling)

## 🛠️ Setup & Run

### Development (using .NET Aspire)

The easiest way to run the entire stack for development is using the Aspire AppHost:

1. Clone the repository.
2. Navigate to the root directory.
3. Run the AppHost:
   ```powershell
   dotnet run --project Overflow.AppHost
   ```
4. Access the **Aspire Dashboard** (usually at `http://localhost:18888` or as indicated in the console output) to see service logs, endpoints, and health.

### Production / Infrastructure (Docker Compose)

The project also includes infrastructure definitions in the `infra` directory:

1. Navigate to the `infra` folder:
   ```powershell
   cd infra
   ```
2. Configure the `.env` file with necessary passwords and image names (see [Environment Variables](#environment-variables)).
3. Start the infrastructure:
   ```powershell
   docker-compose up -d
   ```

## 🏗️ Project Structure

- `Overflow.AppHost`: The .NET Aspire orchestration project.
- `Overflow.ServiceDefaults`: Common configurations for service discovery, resilience, and observability.
- `QuestionService`: Handles question-related business logic and data.
- `SearchService`: Handles full-text search indexing and querying via Typesense.
- `Contracts` & `Contracts2`: Shared data contracts between services.
- `infra`: Infrastructure configuration files, including `docker-compose.yaml` and `.env`.

## ⚙️ Environment Variables

The application relies on several environment variables, primarily managed via the Aspire AppHost or the `.env` file in the `infra` directory.

Key parameters include:
- `KEYCLOAK_PASSWORD`: Password for Keycloak admin.
- `MESSAGING_PASSWORD`: Password for RabbitMQ.
- `POSTGRES_PASSWORD`: Password for the PostgreSQL database.
- `TYPESENSE_API_KEY`: API Key for the Typesense search engine.
- `QUESTION_SVC_IMAGE` / `SEARCH_SVC_IMAGE`: Docker image names for services (used in compose).
- `QUESTION_SVC_PORT` / `SEARCH_SVC_PORT`: Exposed ports for services.

## 📡 API Endpoints (Gateway)

The **Gateway** (YARP) runs on port `8001` by default in development.

- **Questions:** `http://localhost:8001/questions/`
- **Tags:** `http://localhost:8001/tags/`
- **Search:** `http://localhost:8001/search/`
  - `GET /search?query=[tag]keyword` - Search with optional tag filter.
  - `GET /search/similar-titles?query=...` - Search for similar question titles.

## 🧪 Tests

- TODO: Implement unit and integration tests. No tests were detected in the current repository structure.

## 📜 License

- TODO: Add license information.
