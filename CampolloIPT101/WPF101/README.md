# WPF101 Motor Shop - MVVM CRUD Application

A complete WPF application following MVVM pattern with Entity Framework Core and SQL Server LocalDB.

## Project Structure

### WPF101Domain
Contains business logic and domain models:
- **Commands**: CreateVehicleCommand, UpdateVehicleCommand, DeleteVehicleCommand
- **Models**: Vehicle
- **Queries**: GetAllVehiclesQuery

### WPF101Framework
Contains data access layer:
- **Commands**: Command handlers for Create, Update, Delete operations
- **DTOs**: VehicleDto (database entity)
- **Queries**: GetAllVehiclesQueryHandler
- **DbContext**: VehiclesDbContext
- **DbContextFactory**: VehiclesDbContextFactory

### WPF101
Main WPF application:
- **Commands**: AsyncRelayCommand
- **HostBuilders**: Dependency injection configuration
- **Stores**: VehiclesStore (state management)
- **ViewModels**: MainViewModel, VehicleViewModel
- **Views**: MainWindow.xaml
- **appsettings.json**: Configuration with connection string

## Setup Instructions

### 1. Configure Connection String

Open `WPF101/appsettings.json` and update the connection string if needed:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WPF101MotorShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
  }
}
```

**To get your connection string from Visual Studio:**
1. Open **View** → **SQL Server Object Explorer**
2. Expand your SQL Server instance (e.g., `(localdb)\MSSQLLocalDB`)
3. Right-click on **Databases** → **Add New Database**
4. Name it `WPF101MotorShop`
5. Right-click the database → **Properties**
6. Copy the **Connection String** from the Properties window
7. Paste it into `appsettings.json`

### 2. Create Database Migration

Open **Package Manager Console** in Visual Studio:
- **Tools** → **NuGet Package Manager** → **Package Manager Console**

Set the default project to **WPF101** and run:

```powershell
Add-Migration InitialCreate -Project WPF101Framework -StartupProject WPF101
```

### 3. Update Database

Apply the migration to create the database:

```powershell
Update-Database -Project WPF101Framework -StartupProject WPF101
```

### 4. Build and Run

1. Set **WPF101** as the startup project
2. Build the solution (Ctrl+Shift+B)
3. Run the application (F5)

## Features

### CRUD Operations

- **Create**: Add new vehicles using the "Add New Vehicle" form
- **Read**: View all vehicles in the DataGrid (automatically loaded on startup)
- **Update**: Select a vehicle, edit its details in the "Edit Selected Vehicle" form, and click Update
- **Delete**: Select a vehicle and click Delete (with confirmation dialog)
- **Refresh**: Reload data from the database

### Data Validation

- Make and Model fields are required to add a vehicle
- Update and Delete buttons are only enabled when a vehicle is selected
- Only user-added vehicles can be edited or deleted (displayed in the grid)

## Architecture

### MVVM Pattern
- **Model**: Domain models in WPF101Domain
- **View**: XAML files with data binding
- **ViewModel**: ViewModels with INotifyPropertyChanged

### Dependency Injection
- Uses Microsoft.Extensions.Hosting
- Services registered in HostBuilders
- DbContext factory pattern for proper disposal

### Command/Query Separation
- Commands for write operations (Create, Update, Delete)
- Queries for read operations (GetAll)
- Handlers in Framework layer

### Store Pattern
- VehiclesStore manages application state
- Events for state changes (VehicleAdded, VehicleUpdated, etc.)
- Async operations for database access

## Technologies

- .NET 9.0
- WPF (Windows Presentation Foundation)
- Entity Framework Core 9.0
- SQL Server LocalDB
- Microsoft.Extensions.Hosting
- Microsoft.Extensions.Configuration

## Troubleshooting

### Migration Issues

If you get errors during migration, ensure:
1. WPF101Framework is selected as the project in Package Manager Console
2. WPF101 is set as the startup project
3. Connection string in appsettings.json is correct

### Database Connection Issues

If the app can't connect to the database:
1. Verify SQL Server LocalDB is installed
2. Check the connection string matches your LocalDB instance
3. Ensure the database was created via Update-Database

### Build Errors

If you get build errors:
1. Clean the solution (Build → Clean Solution)
2. Restore NuGet packages (right-click solution → Restore NuGet Packages)
3. Rebuild the solution (Build → Rebuild Solution)
