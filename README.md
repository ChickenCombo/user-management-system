# User Management System

This demo project highlights the implementation of a .NET 9 Web API following the Clean Architecture pattern, showcasing best practices for scalable and maintainable code. It incorporates Entity Framework Core for database management and migration handling, and features comprehensive OpenAPI documentation Scalar.

## Project Structure

```
UserManagement/
├── API/
│   ├── Controllers/     - API endpoints
│   ├── DTOs/            - Data Transfer Objects
│   ├── Mappers/         - Entity-DTO mapping
│   └── Program.cs       - Entry point
│
├── Core/
│   ├── Entities/        - Domain models
│   └── Interfaces/      - Repository contracts
│
├── Infrastructure/
│   ├── Data/            - Database context
│   ├── Migrations/      - EF Core migrations
│   └── Repositories/    - Data access implementations
│
└── UserManagement.sln   - Solution file
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
