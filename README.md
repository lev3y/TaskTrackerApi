# Task Tracker API

A REST API built with ASP.NET Core Web API and .NET 8 for managing projects and tasks.

## Features
- Project CRUD operations
- Task CRUD operations
- Retrieve tasks by project
- Task status management (`Todo`, `InProgress`, `Done`)
- Request validation
- SQLite database with Entity Framework Core
- Automatic sample data seeding when the database is empty

## How to Run
1. Install the .NET 8 SDK
2. Open a terminal in the project folder.
3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Start the API:

   ```bash
   dotnet run
   ```

5. Open the Swagger URL shown in the terminal to test the API.

## Notes
- Data is stored in a local SQLite database (`tasktracker.db`) and persists between application restarts.
- Sample projects and tasks are inserted automatically when the database is empty.
- The database file is not included in the repository; migrations create the database schema.