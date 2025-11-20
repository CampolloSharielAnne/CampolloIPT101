# Troubleshooting Guide - "Hindi Sya Maka-Add"

## ✅ Database Setup Complete

The database has been successfully created with the following:
- ✅ Migration created: `InitialCreate`
- ✅ Database updated: `WPF101MotorShop`
- ✅ Table created: `Vehicles` with columns (Id, Make, Model, Year, Price)

## Common Issues When Adding Vehicles

### Issue 1: Button is Disabled / Grayed Out

**Cause**: The Add button requires both Make and Model fields to be filled.

**Solution**:
1. Type something in the **Make** field (e.g., "Toyota")
2. Type something in the **Model** field (e.g., "Camry")
3. The Add button should now be enabled

### Issue 2: Nothing Happens When Clicking Add

**Possible Causes**:

#### A. Database Connection Error

Check if you see an error message popup. If yes:

1. Open **SQL Server Object Explorer** (View → SQL Server Object Explorer)
2. Expand `(localdb)\MSSQLLocalDB`
3. Look for `WPF101MotorShop` database
4. If it doesn't exist, run in Package Manager Console:
   ```powershell
   Update-Database -Project WPF101Framework -StartupProject WPF101
   ```

#### B. Application Not Running Latest Build

1. Stop the application (if running)
2. Clean solution: **Build** → **Clean Solution**
3. Rebuild: **Build** → **Rebuild Solution**
4. Run again: **F5**

#### C. Connection String Issue

Open `WPF101/appsettings.json` and verify the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WPF101MotorShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
  }
}
```

### Issue 3: Error Message Appears

If you see an error popup, read the message carefully. Common errors:

#### "Cannot open database"
- SQL Server LocalDB is not running
- Solution: Restart Visual Studio or run `sqllocaldb start MSSQLLocalDB` in command prompt

#### "Unable to resolve service"
- Dependency injection issue
- Solution: Rebuild the solution

#### "Object reference not set to an instance"
- Check if all handlers are registered in `AddServicesHostBuilderExtensions.cs`

## Testing Steps

### Step 1: Verify Database Exists

1. Open **SQL Server Object Explorer**
2. Expand `(localdb)\MSSQLLocalDB` → **Databases**
3. You should see `WPF101MotorShop`
4. Expand it → **Tables**
5. You should see `dbo.Vehicles`
6. Right-click the table → **View Data** (should be empty initially)

### Step 2: Test Adding a Vehicle

1. Run the application (F5)
2. Fill in the form:
   - Make: `Toyota`
   - Model: `Camry`
   - Year: `2024`
   - Price: `25000`
3. Click **Add**
4. The vehicle should appear in the grid above

### Step 3: Verify in Database

1. Go back to **SQL Server Object Explorer**
2. Right-click `dbo.Vehicles` → **View Data**
3. You should see your added vehicle with an auto-generated Id

### Step 4: Test Update

1. Click on the vehicle in the grid (it becomes selected)
2. The right form "Edit Selected Vehicle" should populate
3. Change the Price to `26000`
4. Click **Update**
5. You should see a success message
6. Verify the change in the grid

### Step 5: Test Delete

1. Select a vehicle in the grid
2. Click **Delete** in the right form
3. Confirm the deletion
4. The vehicle should disappear from the grid

## Debug Mode

If you want to see what's happening:

1. Open `WPF101/ViewModels/MainViewModel.cs`
2. Put a breakpoint on line in `AddVehicle()` method (click left margin)
3. Run in debug mode (F5)
4. Fill the form and click Add
5. The debugger will stop at your breakpoint
6. Press F10 to step through the code and see what happens

## Checking Logs

If the application crashes on startup, check the Output window:
1. **View** → **Output**
2. Select "Debug" from the dropdown
3. Look for error messages

## Still Not Working?

### Quick Reset

1. Stop the application
2. Delete the database:
   ```powershell
   dotnet ef database drop --project WPF101Framework --startup-project WPF101
   ```
3. Recreate it:
   ```powershell
   dotnet ef database update --project WPF101Framework --startup-project WPF101
   ```
4. Rebuild and run

### Check All Files Are Saved

- Press **Ctrl+Shift+S** to save all files
- Rebuild the solution

### Verify NuGet Packages

1. Right-click solution → **Restore NuGet Packages**
2. Wait for completion
3. Rebuild

## Contact Points

If you're still having issues, check:

1. **Output Window** (View → Output) - Shows build and runtime errors
2. **Error List** (View → Error List) - Shows compilation errors
3. **Debug Output** - Shows exception details when debugging

## Common Error Messages and Solutions

| Error Message | Solution |
|--------------|----------|
| "Cannot locate resource 'mainwindow.xaml'" | Rebuild the solution |
| "Unable to resolve service for type 'DbContextOptions'" | Check AddDbContextHostBuilderExtensions.cs |
| "A network-related error occurred" | Check SQL Server LocalDB is running |
| "Login failed for user" | Check connection string in appsettings.json |
| "Invalid object name 'Vehicles'" | Run Update-Database command |

## Success Indicators

You know it's working when:
- ✅ Application starts without errors
- ✅ Grid is visible (even if empty)
- ✅ Forms are visible and editable
- ✅ Add button becomes enabled when Make and Model are filled
- ✅ Clicking Add shows the vehicle in the grid immediately
- ✅ Vehicle appears in SQL Server Object Explorer → View Data
