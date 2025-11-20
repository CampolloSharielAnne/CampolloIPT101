# Quick Reference Card

## 🚀 Quick Start

```powershell
# 1. Update connection string in appsettings.json
# 2. Run migration
Update-Database -Project WPF101Framework -StartupProject WPF101

# 3. Run app
Press F5
```

## 📝 Change Connection String

**File**: `WPF101/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING_HERE"
  }
}
```

**No rebuild needed!** Just run the app.

## 🔧 Common Commands

### Migrations
```powershell
# Create migration
Add-Migration MigrationName -Project WPF101Framework -StartupProject WPF101

# Apply migration
Update-Database -Project WPF101Framework -StartupProject WPF101

# Remove last migration
Remove-Migration -Project WPF101Framework -StartupProject WPF101

# List migrations
Get-Migration -Project WPF101Framework -StartupProject WPF101
```

### Database
```powershell
# Drop database
dotnet ef database drop --project WPF101Framework --startup-project WPF101 --force

# Check connection
dotnet ef dbcontext info --project WPF101Framework --startup-project WPF101
```

### Build
```powershell
# Clean
dotnet clean

# Build
dotnet build WPF101/WPF101.csproj

# Run
dotnet run --project WPF101/WPF101.csproj
```

## 🎯 Common Connection Strings

### LocalDB (Default)
```
Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WPF101MotorShop;Integrated Security=True;Encrypt=False;
```

### SQL Express
```
Server=.\\SQLEXPRESS;Database=WPF101MotorShop;Trusted_Connection=True;TrustServerCertificate=True;
```

### SQL Server (Local)
```
Server=localhost;Database=WPF101MotorShop;Trusted_Connection=True;TrustServerCertificate=True;
```

### SQL Server (Remote)
```
Server=192.168.1.100;Database=WPF101MotorShop;User Id=sa;Password=YourPassword;TrustServerCertificate=True;
```

## 📂 Project Structure

```
WPF101Domain/          - Business logic (Commands, Models, Queries)
WPF101Framework/       - Data access (EF Core, DbContext, Handlers)
WPF101/                - WPF app (ViewModels, Views, DI)
  ├── appsettings.json - Connection string HERE
  ├── App.xaml.cs      - DI setup
  └── MainWindow.xaml  - UI
```

## 🐛 Troubleshooting

### White Screen
1. Check Output window (View → Output, select "Debug")
2. Look for binding errors
3. Check if DataContext is set

### Can't Add
1. Check if Make and Model are filled
2. Check database connection
3. Check for error popups

### Database Error
1. Verify connection string in appsettings.json
2. Check if database exists
3. Run Update-Database

### Build Error
1. Clean solution
2. Restore NuGet packages
3. Rebuild

## 📚 Documentation Files

- **QUICK_START.md** - 4-step setup guide
- **README.md** - Complete documentation
- **MIGRATION_GUIDE.md** - EF Core migrations
- **CONNECTION_STRING_GUIDE.md** - How to change connection string
- **TROUBLESHOOTING.md** - Common issues
- **DEBUG_STEPS.md** - Debugging guide
- **WHITE_SCREEN_FIX.md** - White screen solutions
- **PROJECT_STRUCTURE.md** - Architecture details
- **STATUS.md** - Current status

## ✅ Checklist

Before running:
- [ ] Connection string in appsettings.json
- [ ] Database exists
- [ ] Migrations applied (Update-Database)
- [ ] WPF101 is startup project
- [ ] Solution builds without errors

## 🎉 Features

- ✅ Full CRUD (Create, Read, Update, Delete)
- ✅ MVVM architecture
- ✅ Entity Framework Core
- ✅ SQL Server database
- ✅ Dependency Injection
- ✅ Async operations
- ✅ Flexible connection string
- ✅ Migration support
- ✅ Data validation
- ✅ Event-driven updates

## 💡 Pro Tips

1. **Connection string** - Change in appsettings.json, no rebuild needed
2. **Debug mode** - Press F5 (not Ctrl+F5) to see errors
3. **Breakpoints** - Click left margin in code to debug
4. **Output window** - View → Output for detailed logs
5. **SQL Server Object Explorer** - View → SQL Server Object Explorer to see database
