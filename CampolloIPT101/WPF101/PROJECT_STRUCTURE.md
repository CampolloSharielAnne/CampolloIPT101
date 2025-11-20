# WPF101 Project Structure

## Complete File Organization

```
WPF101 Solution
│
├── WPF101Domain/                          # Domain Layer (Business Logic)
│   ├── Commands/
│   │   ├── CreateVehicleCommand.cs       # Command for creating vehicles
│   │   ├── UpdateVehicleCommand.cs       # Command for updating vehicles
│   │   └── DeleteVehicleCommand.cs       # Command for deleting vehicles
│   ├── Models/
│   │   └── Vehicle.cs                    # Domain model
│   ├── Queries/
│   │   └── GetAllVehiclesQuery.cs        # Query for fetching all vehicles
│   └── WPF101Domain.csproj
│
├── WPF101Framework/                       # Data Access Layer
│   ├── Commands/
│   │   ├── CreateVehicleCommandHandler.cs
│   │   ├── UpdateVehicleCommandHandler.cs
│   │   └── DeleteVehicleCommandHandler.cs
│   ├── DTOs/
│   │   └── VehicleDto.cs                 # Database entity
│   ├── Queries/
│   │   └── GetAllVehiclesQueryHandler.cs
│   ├── VehiclesDbContext.cs              # EF Core DbContext
│   ├── VehiclesDbContextFactory.cs       # Factory for DbContext
│   ├── WPF101Framework.csproj
│   └── Migrations/                        # (Created after Add-Migration)
│
└── WPF101/                                # Presentation Layer (WPF App)
    ├── Commands/
    │   └── AsyncRelayCommand.cs          # Async command implementation
    ├── HostBuilders/
    │   ├── AddDbContextHostBuilderExtensions.cs
    │   └── AddServicesHostBuilderExtensions.cs
    ├── Stores/
    │   └── VehiclesStore.cs              # Application state management
    ├── ViewModels/
    │   ├── ViewModelBase.cs              # Base class for ViewModels
    │   ├── MainViewModel.cs              # Main window ViewModel
    │   └── VehicleViewModel.cs           # Vehicle item ViewModel
    ├── Views/                             # (Optional folder for organization)
    ├── App.xaml                          # Application definition
    ├── App.xaml.cs                       # Application startup & DI setup
    ├── MainWindow.xaml                   # Main window UI
    ├── MainWindow.xaml.cs                # Main window code-behind
    ├── appsettings.json                  # Configuration (connection string)
    ├── WPF101.csproj
    ├── README.md                         # Setup instructions
    ├── MIGRATION_GUIDE.md                # Database migration guide
    └── PROJECT_STRUCTURE.md              # This file
```

## Layer Responsibilities

### Domain Layer (WPF101Domain)
- **Purpose**: Contains pure business logic and domain models
- **Dependencies**: None (no external dependencies)
- **Contains**:
  - Domain models (Vehicle)
  - Command definitions (what operations can be performed)
  - Query definitions (what data can be retrieved)

### Framework Layer (WPF101Framework)
- **Purpose**: Handles data persistence and database operations
- **Dependencies**: 
  - WPF101Domain
  - Entity Framework Core
  - SQL Server provider
- **Contains**:
  - DTOs (database entities)
  - Command handlers (how to execute commands)
  - Query handlers (how to retrieve data)
  - DbContext and factory

### Presentation Layer (WPF101)
- **Purpose**: User interface and user interaction
- **Dependencies**:
  - WPF101Domain
  - WPF101Framework
  - Microsoft.Extensions.Hosting
  - Microsoft.Extensions.Configuration
- **Contains**:
  - ViewModels (presentation logic)
  - Views (XAML UI)
  - Commands (UI command implementations)
  - Stores (application state)
  - Dependency injection setup

## Data Flow

### Read Operation (Query)
```
User clicks Refresh
    ↓
MainViewModel.RefreshCommand
    ↓
VehiclesStore.Load()
    ↓
GetAllVehiclesQueryHandler.Handle()
    ↓
VehiclesDbContext queries database
    ↓
Returns List<Vehicle>
    ↓
VehiclesStore updates state
    ↓
VehiclesStore.VehiclesLoaded event fires
    ↓
MainViewModel updates ObservableCollection
    ↓
UI updates via data binding
```

### Write Operation (Command)
```
User fills form and clicks Add
    ↓
MainViewModel.AddCommand
    ↓
VehiclesStore.Add(CreateVehicleCommand)
    ↓
CreateVehicleCommandHandler.Handle()
    ↓
VehiclesDbContext saves to database
    ↓
Returns new Vehicle with Id
    ↓
VehiclesStore updates state
    ↓
VehiclesStore.VehicleAdded event fires
    ↓
MainViewModel adds to ObservableCollection
    ↓
UI updates via data binding
```

## Key Design Patterns

### MVVM (Model-View-ViewModel)
- **Model**: Domain models in WPF101Domain
- **View**: XAML files (MainWindow.xaml)
- **ViewModel**: ViewModels with INotifyPropertyChanged

### Repository Pattern
- VehiclesStore acts as a repository
- Abstracts data access from ViewModels

### Command/Query Separation (CQS)
- Commands: Modify state (Create, Update, Delete)
- Queries: Read state (GetAll)

### Factory Pattern
- VehiclesDbContextFactory creates DbContext instances
- Ensures proper disposal and connection management

### Dependency Injection
- Services registered in HostBuilders
- Constructor injection throughout the app

### Observer Pattern
- VehiclesStore publishes events
- ViewModels subscribe to state changes

## NuGet Packages

### WPF101Domain
- None (pure .NET)

### WPF101Framework
- Microsoft.EntityFrameworkCore (9.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
- Microsoft.EntityFrameworkCore.Design (9.0.0)

### WPF101
- Microsoft.EntityFrameworkCore (9.0.0)
- Microsoft.EntityFrameworkCore.Design (9.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
- Microsoft.Extensions.Configuration (9.0.0)
- Microsoft.Extensions.Configuration.Json (9.0.0)
- Microsoft.Extensions.Hosting (9.0.0)

## Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string here"
  }
}
```

### Important Notes
- `appsettings.json` is set to **Copy to Output Directory: Always**
- Connection string is flexible and can be changed without recompiling
- Uses SQL Server LocalDB by default (included with Visual Studio)

## Building the Solution

```bash
# Restore packages
dotnet restore

# Build all projects
dotnet build

# Build specific project
dotnet build WPF101/WPF101.csproj

# Run the application
dotnet run --project WPF101
```

## Next Steps

1. ✅ Projects created with proper structure
2. ✅ All files in place
3. ⏭️ Configure connection string in appsettings.json
4. ⏭️ Run migrations (see MIGRATION_GUIDE.md)
5. ⏭️ Build and run the application
6. ⏭️ Test CRUD operations
