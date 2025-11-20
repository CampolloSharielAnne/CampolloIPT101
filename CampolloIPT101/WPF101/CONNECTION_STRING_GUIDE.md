# Connection String Guide

## ✅ YES! Pwede Palitan Anytime

Ang connection string ay naka-store sa `appsettings.json` - **hindi hardcoded** sa code. Pwede mo palitan without recompiling!

## 📝 How to Change Connection String

### Step 1: Open appsettings.json

Location: `WPF101/appsettings.json`

Current content:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WPF101MotorShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
  }
}
```

### Step 2: Replace with Your Connection String

Just paste your new connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_NEW_CONNECTION_STRING_HERE"
  }
}
```

### Step 3: Run Migrations (If New Database)

If you're connecting to a **new/empty database**, run:

```powershell
# In Package Manager Console
Update-Database -Project WPF101Framework -StartupProject WPF101
```

This creates the `Vehicles` table in your new database.

### Step 4: Run the App

That's it! Just run (F5) and it will use the new connection string.

## 🎯 Common Connection String Examples

### 1. SQL Server LocalDB (Current)
```
Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WPF101MotorShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
```

### 2. SQL Server Express (Local)
```
Server=.\\SQLEXPRESS;Database=WPF101MotorShop;Trusted_Connection=True;TrustServerCertificate=True;
```

### 3. SQL Server (Named Instance)
```
Server=YOUR_SERVER_NAME\\INSTANCE_NAME;Database=WPF101MotorShop;Trusted_Connection=True;TrustServerCertificate=True;
```

### 4. SQL Server (With Username/Password)
```
Server=YOUR_SERVER_NAME;Database=WPF101MotorShop;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;
```

### 5. SQL Server (Remote Server)
```
Server=192.168.1.100;Database=WPF101MotorShop;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;
```

### 6. Azure SQL Database
```
Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=WPF101MotorShop;Persist Security Info=False;User ID=yourusername;Password=yourpassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

## 📋 How to Get Your Connection String

### From SQL Server Object Explorer (Visual Studio)

1. Open **View** → **SQL Server Object Explorer**
2. Expand your SQL Server instance
3. Right-click on your database
4. Select **Properties**
5. Find **Connection String** in the Properties window
6. Copy the entire string
7. Paste into `appsettings.json`

### From SQL Server Management Studio (SSMS)

1. Connect to your SQL Server
2. Right-click on the database
3. Select **Properties**
4. Go to **Connection Strings** tab
5. Copy the connection string
6. Paste into `appsettings.json`

### Manual Construction

Format:
```
Server=SERVER_NAME;Database=DATABASE_NAME;Trusted_Connection=True;TrustServerCertificate=True;
```

Replace:
- `SERVER_NAME` - Your SQL Server name (e.g., `localhost`, `.\SQLEXPRESS`, `192.168.1.100`)
- `DATABASE_NAME` - Your database name (e.g., `WPF101MotorShop`)

## ⚠️ Important Notes

### 1. Escape Backslashes in JSON

In `appsettings.json`, use **double backslash** `\\`:

❌ Wrong:
```json
"Data Source=(localdb)\MSSQLLocalDB"
```

✅ Correct:
```json
"Data Source=(localdb)\\MSSQLLocalDB"
```

### 2. Database Must Exist

The database must exist before running the app. Either:

**Option A**: Create database manually in SQL Server
```sql
CREATE DATABASE WPF101MotorShop;
```

**Option B**: Let migrations create it
```powershell
Update-Database -Project WPF101Framework -StartupProject WPF101
```

### 3. Run Migrations After Changing Database

If you switch to a **different database**, run migrations:

```powershell
# This creates the Vehicles table in the new database
Update-Database -Project WPF101Framework -StartupProject WPF101
```

### 4. No Need to Rebuild

After changing `appsettings.json`:
- ❌ No need to rebuild
- ❌ No need to recompile
- ✅ Just run the app (F5)

The file is copied to output directory automatically.

## 🔧 Testing New Connection String

### Step 1: Update appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_NEW_CONNECTION_STRING"
  }
}
```

### Step 2: Test Connection
```powershell
# In Package Manager Console
dotnet ef dbcontext info --project WPF101Framework --startup-project WPF101
```

Should show:
- Provider name: Microsoft.EntityFrameworkCore.SqlServer
- Database name: Your database name
- Data source: Your server name

### Step 3: Apply Migrations
```powershell
Update-Database -Project WPF101Framework -StartupProject WPF101
```

### Step 4: Run App
```
Press F5
```

## 🎯 Example: Switching from LocalDB to SQL Express

### Current (LocalDB):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WPF101MotorShop;Integrated Security=True;Encrypt=False;"
  }
}
```

### New (SQL Express):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=WPF101MotorShop;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Then Run:
```powershell
Update-Database -Project WPF101Framework -StartupProject WPF101
```

Done! App will now use SQL Express.

## 🔐 Security Best Practices

### For Development:
- Use **Integrated Security** (Windows Authentication)
- No passwords in connection string
```
Trusted_Connection=True;
```

### For Production:
- Use **User Secrets** or **Environment Variables**
- Don't commit passwords to source control

Example with User Secrets:
```powershell
dotnet user-secrets init --project WPF101
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING" --project WPF101
```

## 📊 Multiple Connection Strings

You can have multiple connection strings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=WPF101MotorShop;Trusted_Connection=True;",
    "ProductionConnection": "Server=PROD_SERVER;Database=WPF101MotorShop;User Id=sa;Password=xxx;",
    "TestConnection": "Server=TEST_SERVER;Database=WPF101MotorShop_Test;Trusted_Connection=True;"
  }
}
```

To use a different one, update `AddDbContextHostBuilderExtensions.cs`:
```csharp
var connectionString = context.Configuration.GetConnectionString("ProductionConnection");
```

## ✅ Summary

**YES, pwede palitan anytime!**

1. ✅ Open `WPF101/appsettings.json`
2. ✅ Replace connection string
3. ✅ Run migrations if new database: `Update-Database`
4. ✅ Run app (F5)

**No recompiling needed!** The app reads `appsettings.json` at runtime.

This is one of the main advantages of this architecture - **flexible configuration** without touching code! 🎉
