# Getting Started

This guide provides step-by-step instructions to set up and run the `Ambev.DeveloperEvaluation` project locally. The project is a sales management API built with .NET 8, PostgreSQL.

## Prerequisites

Ensure you have the following tools installed on your system:
- **.NET 8 SDK**: For local development or manual builds (optional if using Docker).
- **Git**: To clone the repository.
- **Visual Studio Code** or **Visual Studio 2022**: Ide for code, build, tests, debugging.
- **PostgreSQL**: Required for running the application database.  

Verify installations:
```sh
dotnet --version
git --version
```

### Step 1: Clone the Repository
Clone the project from GitHub:
```bash
git clone https://github.com/mvtorrecilha/developer-evaluation.git
cd abi-gth-omnia-developer-evaluation
```

### Step 2: Create database in PostgreSQL
This project uses [Entity Framework Core Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/) for managing database schema changes over time.

Enter the directory `src` and execute command below:
```bash
dotnet ef database update --project Ambev.DeveloperEvaluation.ORM --startup-project Ambev.DeveloperEvaluation.WebApi
```

### Step 3: Start the Services
Restore the dependencies:
```bash
dotnet restore
```

Run the application:
```bash
dotnet run
```

### Step 4: Access the API
Once the services are running, access the Swagger UI to interact with the API:

URL: https://localhost:7181/swagger/index.html
The API runs on HTTPS (7180) by default, but HTTP (5119) is also available.

