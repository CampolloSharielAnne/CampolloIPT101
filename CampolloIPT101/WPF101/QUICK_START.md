# WPF101 Motor Shop - Quick Start Guide

## ✅ What's Been Created

Your WPF101 project now has a complete MVVM + CRUD architecture with three layers:

1. **WPF101Domain** - Business logic layer
2. **WPF101Framework** - Data access layer with Entity Framework Core
3. **WPF101** - Presentation layer (WPF application)

## 🚀 Next Steps (In Order)

### Step 1: Configure Connection String

1. Open **View** → **SQL Server Object Explorer** in Visual Studio
2. Expand `(localdb)\MSSQLLocalDB`
3. Right-click **Databases** → **Add New Database**
4. Name it: `WPF101MotorShop`
5. Right-click the new database → **Properties**
6. Copy the **Connection String**
7. Open `WPF101/appsettings.json` and paste your connection string

**Or use the default connection string** (already in appsettings.json):
```
Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WPF101MotorShop;...
```

### Step 2: Create Database Migration

Open **Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console):

1. Set **Default project** to: `WPF101Framework`
2. Ensure **WPF101** is the startup project (right-click → Set as Startup Project)
3. Run:

```powershell
Add-Migration InitialCreate
```

### Step 3: Apply Migration to Database

In the same Package Manager Console:

```powershell
Update-Database
```

This creates the `Vehicles` table in your database.

### Step 4: Build and Run

1. Press **F5** or click **Start**
2. The application will launch with an empty vehicle list

## 🎯 Using the Application

### Add a Vehicle
1. Fill in the "Add New Vehicle" form (left side)
   - Make (e.g., "Toyota")
   - Model (e.g., "Camry")
   - Year (e.g., 2024)
   - Price (e.g., 25000)
2. Click **Add**
3. The vehicle appears in the grid

### Edit a Vehicle
1. Click on a vehicle in the grid to select it
2. The "Edit Selected Vehicle" form (right side) populates
3. Modify any fields
4. Click **Update**
5. Changes are saved to the database

### Delete a Vehicle
1. Select a vehicle in the grid
2. Click **Delete** in the edit form
3. Confirm the deletion
4. The vehicle is removed from the database

### Refresh Data
- Click **Refresh** to reload all vehicles from the database

## 📁 Project Structure

```
WPF101Domain/
├── Commands/          # CreateVehicleCommand, UpdateVehicleCommand, DeleteVehicleCommand
├── Models/            # Vehicle
└── Queries/           # GetAllVehiclesQuery

WPF101Framework/
├── Commands/          # Command handlers (Create, Update, Delete)
├── DTOs/              # VehicleDto (database entity)
├── Queries/           # GetAllVehiclesQueryHandler
├── VehiclesDbContext.cs
└── VehiclesDbContextFactory.cs

WPF101/
├── Commands/          # AsyncRelayCommand
├── HostBuilders/      # Dependency injection setup
├── Stores/            # VehiclesStore (state management)
├── ViewModels/        # MainViewModel, VehicleViewModel
├── MainWindow.xaml    # UI
└── appsettings.json   # Configuration
```

## 🔧 Key Features

✅ **MVVM Pattern** - Clean separation of concerns
✅ **Entity Framework Core** - Modern ORM with migrations
✅ **Dependency Injection** - Using Microsoft.Extensions.Hosting
✅ **Async Operations** - All database operations are async
✅ **Data Binding** - Two-way binding with INotifyPropertyChanged
✅ **Flexible Connection String** - Configured in appsettings.json
✅ **Command/Query Separation** - Clear distinction between reads and writes
✅ **Store Pattern** - Centralized state management with events

## 📚 Additional Documentation

- **README.md** - Detailed setup instructions and architecture overview
- **MIGRATION_GUIDE.md** - Complete guide to Entity Framework migrations
- **PROJECT_STRUCTURE.md** - In-depth explanation of the project organization

## ⚠️ Troubleshooting

### "No DbContext was found"
- Make sure WPF101 is the startup project
- Ensure WPF101Framework is selected in Package Manager Console

### "Cannot open database"
- Verify SQL Server LocalDB is installed (comes with Visual Studio)
- Check the connection string in appsettings.json

### Build Errors
1. Clean Solution (Build → Clean Solution)
2. Rebuild Solution (Build → Rebuild Solution)

## 🎉 You're Ready!

Your WPF101 Motor Shop application is now fully configured with:
- ✅ Three-layer architecture (Domain, Framework, Presentation)
- ✅ MVVM pattern implementation
- ✅ Entity Framework Core with SQL Server
- ✅ Full CRUD operations
- ✅ Flexible configuration
- ✅ Migration support

Just follow the 4 steps above and you'll have a working application!
