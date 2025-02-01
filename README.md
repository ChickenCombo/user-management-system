# User Management System

This demo project highlights the implementation of a .NET 9 Web API following the Clean Architecture pattern, showcasing best practices for scalable and maintainable code. It incorporates Entity Framework Core for database management and migration handling, and features comprehensive OpenAPI documentation Scalar.

## Project Structure

```
UserManagement/
├── API/
│   ├── Endpoints/       - API route definitions and configurations
│   ├── Handlers/        - Request handlers for processing API requests
│   ├── DTOs/            - Data Transfer Objects for communication with clients
│   ├── Mappers/         - Logic for mapping entities to DTOs
│   ├── Transformers/    - Transformers for OpenAPI-related objects.
│   ├── Helpers/         - Utility classes and functions used across the API layer
│   ├── BuilderServices/ - Service registrations and dependency configuration
│   └── Program.cs       - Application entry point and API setup
│
├── Core/
│   ├── Entities/        - Core domain models representing business objects
│   ├── Interfaces/      - Repository and service contracts for core operations
│   └── Queries/         - Query parameters and filtering logic
│
├── Infrastructure/
│   ├── Data/            - Database context and EF Core configuration
│   ├── Migrations/      - EF Core database migrations
│   ├── QueryBuilders/   - Query construction and filtering logic for data queries
│   ├── Repositories/    - Data access implementations (e.g., repository pattern)
│   └── Interfaces/      - Contracts for infrastructure-related services
│
└── UserManagement.sln   - Solution file for the project
```

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server

### Project Setup

Clone the repository

```
git clone https://github.com/ChickenCombo/user-management-system.git
```

Navigate into the project directory:

```
cd user-management-system
```

### Setup Database

1. Update the `appsettings.json` file with your SQL Server connection string:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;"
}
```

2. Run the migrations to setup the database schema:

```
dotnet ef database update --project Infrastructure --startup-project API
```

### Run the API

Start the application (use `watch` for development):

```
dotnet run --project API/API.csproj
```

The application will run on http://localhost:5035/ by default.
