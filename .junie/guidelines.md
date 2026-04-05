# Project Guidelines - Overflow

This document provides essential information for developers working on the Overflow project.

## 🏗️ Build & Configuration

Overflow is a distributed application orchestrated using **.NET Aspire**.

### Prerequisites
- .NET 10 SDK
- Docker Desktop (required for Aspire resources)
- .NET Aspire workload (`dotnet workload install aspire`)

### Local Development
To run the entire solution including infrastructure (PostgreSQL, RabbitMQ, Typesense, Keycloak):
1. Navigate to the root directory.
2. Run the AppHost:
   ```powershell
   dotnet run --project Overflow.AppHost
   ```
3. Access the **Aspire Dashboard** at `http://localhost:18888` to monitor services.

### Infrastructure (Docker Compose)
A `docker-compose.yaml` is available in the `infra` directory for running infrastructure components separately.
```powershell
cd infra
docker-compose up -d
```
Note: Ensure you have a `.env` file in the `infra` directory with required secrets.

---

## 🧪 Testing Information

### Test Structure
Tests are located in the `Tests` directory. Currently, we use **xUnit** for unit testing.

### Running Tests
To run all tests in the solution:
```powershell
dotnet test
```

### Adding New Tests
1. **Unit Tests**: Add tests to the `Overflow.UnitTests` project.
2. **References**: Ensure the test project references the service project being tested (e.g., `QuestionService`).
3. **Naming**: Use descriptive names like `ClassName_MethodName_ShouldExpectedBehavior`.

#### Example Test
The following is an example of a unit test for `TagListValidator`:
```csharp
using QuestionService.Validators;
using System.ComponentModel.DataAnnotations;

public class TagListValidatorTests
{
    [Theory]
    [InlineData(1, 3, 2, true)]  // 2 tags is valid for 1-3 range
    [InlineData(1, 3, 4, false)] // 4 tags is invalid for 1-3 range
    public void IsValid_ShouldValidateTagCount(int min, int max, int tagCount, bool expected)
    {
        var validator = new TagListValidator(min, max);
        var tags = Enumerable.Range(0, tagCount).Select(i => "tag").ToList();
        var result = validator.GetValidationResult(tags, new ValidationContext(tags));

        if (expected)
            Assert.Equal(ValidationResult.Success, result);
        else
            Assert.NotEqual(ValidationResult.Success, result);
    }
}
```

---

## 🛠️ Additional Development Information

### Architecture Highlights
- **Messaging**: Uses **Wolverine** with RabbitMQ. `QuestionService` publishes messages to the `questions` exchange.
- **Search**: **Typesense** is used for full-text search. The `SearchService` handles indexing and queries.
- **Authentication**: **Keycloak** integration via `Aspire.Keycloak.Authentication`. The realm `overflow` is imported during startup from `infra/realms`.
- **Gateway**: **YARP** is used as the API Gateway (port 8001 in dev).

### Code Style
- Follow standard C# coding conventions.
- Use File-scoped namespaces.
- Primary constructors are encouraged for dependency injection.

### Debugging
- Use the Aspire Dashboard to view logs and traces.
- Since services are distributed, check RabbitMQ management UI (port 15672) to verify message flow if search indexing fails.
