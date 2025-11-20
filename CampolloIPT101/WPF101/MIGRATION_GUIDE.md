# Database Migration Guide

## Quick Start Commands

### Using Package Manager Console (Recommended)

1. Open **Tools** → **NuGet Package Manager** → **Package Manager Console**

2. Set **Default project** dropdown to: `WPF101Framework`

3. Ensure **WPF101** is set as the startup project (right-click project → Set as Startup Project)

4. Run these commands:

```powershell
# Create initial migration
Add-Migration InitialCreate

# Apply migration to database
Update-Database
```

### Using .NET CLI (Alternative)

Open terminal in the solution directory:

```bash
# Create migration
dotnet ef migrations add InitialCreate --project WPF101Framework --startup-project WPF101

# Update database
dotnet ef database update --project WPF101Framework --startup-project WPF101
```

## Connection String Configuration

The connection string is in `WPF101/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WPF101MotorShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
  }
}
```

### How to Get Your Connection String

1. Open **View** → **SQL Server Object Explorer**
2. Expand your LocalDB instance: `(localdb)\MSSQLLocalDB`
3. Right-click **Databases** → **Add New Database**
4. Name: `WPF101MotorShop`
5. Right-click the new database → **Properties**
6. Copy the **Connection String** value
7. Update `appsettings.json` with your connection string

## Database Schema

The migration will create this table:

```sql
CREATE TABLE [dbo].[Vehicles] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Make] NVARCHAR(MAX) NOT NULL,
    [Model] NVARCHAR(MAX) NOT NULL,
    [Year] INT NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL
)
```

## Common Migration Commands

```powershell
# List all migrations
Get-Migration

# Remove last migration (if not applied)
Remove-Migration

# Update to specific migration
Update-Database -Migration MigrationName

# Revert all migrations
Update-Database -Migration 0

# Generate SQL script
Script-Migration

# Add new migration after model changes
Add-Migration DescriptiveNameHere
```

## Troubleshooting

### Error: "No DbContext was found"

**Solution**: Make sure WPF101 is set as the startup project and WPF101Framework is the default project in Package Manager Console.

### Error: "Cannot open database"

**Solution**: 
1. Verify SQL Server LocalDB is installed
2. Check connection string in appsettings.json
3. Try creating the database manually in SQL Server Object Explorer

### Error: "Build failed"

**Solution**:
1. Clean solution: `Build` → `Clean Solution`
2. Rebuild: `Build` → `Rebuild Solution`
3. Try again

### Error: "The term 'Add-Migration' is not recognized"

**Solution**: Install EF Core tools:
```powershell
dotnet tool install --global dotnet-ef
```

## Verifying the Database

After running migrations, verify in SQL Server Object Explorer:

1. Expand `(localdb)\MSSQLLocalDB`
2. Expand `Databases`
3. Find `WPF101MotorShop`
4. Expand `Tables`
5. You should see `dbo.Vehicles`

Right-click the table → **View Data** to see the empty table ready for your CRUD operations!
