# Task Tracker

A project and task management application built with ASP.NET Core Web API, .NET 8, Blazor, and SQLite.

## Features
- Project CRUD operations
- Task CRUD operations
- Retrieve tasks by project
- Task status management (`Todo`, `InProgress`, `Done`)
- Request validation
- SQLite database with Entity Framework Core
- Automatic sample data seeding when the database is empty
- Blazor frontend for managing projects and tasks

## How to Run
1. Install the .NET 8 SDK
2. Open a terminal in the API project folder.
3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Start the API:

   ```bash
   dotnet run
   ```

5. Open another terminal in the frontend project folder.
6. Start the Blazor frontend:

   ```bash
   dotnet run
   ```

7. Open the frontend URL shown in the terminal. Open the Swagger URL to test the API.

## Notes
- Data is stored in a local SQLite database (`tasktracker.db`) and persists between application restarts.
- Sample projects and tasks are inserted automatically when the database is empty.
- The database file is not included in the repository; migrations create the database schema.
- The frontend currently connects to the API at `http://localhost:5285`. Ensure the API runs on this port. If it is different on your machine, please change it in `appsetting.json` - `ApiBaseUrl`.
