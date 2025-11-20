# Debug Steps - Hindi Maka-Add Issue

## ✅ Fixes Applied

1. **Fixed App.xaml namespace** - Changed from `WPF.App` to `WPF101.App`
2. **Removed StartupUri** - Using DI instead
3. **Added CommandManager.InvalidateRequerySuggested()** - Para mag-update ang button state
4. **Added debug messages** - Para makita kung ano nangyayari

## 🔍 Testing Steps

### Step 1: Run the Application

1. Press **F5** in Visual Studio
2. Dapat mag-open ang window without errors

### Step 2: Test Button State

1. **WITHOUT typing anything**:
   - Ang Add button dapat **DISABLED** (grayed out)
   
2. **Type in Make field only**:
   - Type "Toyota"
   - Add button pa rin **DISABLED**
   
3. **Type in Model field**:
   - Type "Camry"
   - Add button dapat **ENABLED** na (clickable)

### Step 3: Test Adding

1. Fill in all fields:
   - Make: `Toyota`
   - Model: `Camry`
   - Year: `2024`
   - Price: `25000`

2. Click **Add** button

3. **Expected behavior**:
   - Popup message: "Adding vehicle: Toyota Camry"
   - Then: "Vehicle added successfully!"
   - Vehicle appears in the grid
   - Form fields clear

### Step 4: Verify in Database

1. Open **SQL Server Object Explorer**
2. Navigate to: `(localdb)\MSSQLLocalDB` → `Databases` → `WPF101MotorShop` → `Tables` → `dbo.Vehicles`
3. Right-click → **View Data**
4. You should see your vehicle with Id = 1

## 🐛 If Still Not Working

### Check 1: Is the Button Enabled?

If the button is **grayed out** even after typing Make and Model:

**Solution A**: Check if text is actually binding
1. Put a breakpoint in `MainViewModel.cs` on line:
   ```csharp
   set { _newMake = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
   ```
2. Run in debug mode (F5)
3. Type in the Make field
4. Debugger should stop at the breakpoint
5. Check if `value` contains your text

**Solution B**: Rebuild completely
```powershell
dotnet clean
dotnet build
```

### Check 2: Does Button Click Do Nothing?

If button is enabled but clicking does nothing:

**Look for error popup**:
- If you see "Error adding vehicle: ..." - read the full message
- Common errors:
  - "Cannot open database" → Database not created
  - "Object reference not set" → DI issue
  - "Unable to resolve service" → Missing registration

**Check Output window**:
1. View → Output
2. Select "Debug" from dropdown
3. Look for exception messages

### Check 3: Application Won't Start

If application crashes on startup:

**Check App.xaml.cs**:
- Make sure namespace is `WPF101`
- Make sure `_host.StartAsync()` is called

**Check connection string**:
- Open `appsettings.json`
- Verify connection string is correct

**Rebuild database**:
```powershell
dotnet ef database drop --project WPF101Framework --startup-project WPF101 --force
dotnet ef database update --project WPF101Framework --startup-project WPF101
```

## 🔧 Manual Testing Commands

### Test Database Connection

Run in Package Manager Console:
```powershell
# Check if database exists
dotnet ef database drop --project WPF101Framework --startup-project WPF101 --dry-run

# List migrations
dotnet ef migrations list --project WPF101Framework --startup-project WPF101

# Check connection
dotnet ef dbcontext info --project WPF101Framework --startup-project WPF101
```

### Test Build

```powershell
# Clean
dotnet clean WPF101/WPF101.csproj

# Restore packages
dotnet restore WPF101/WPF101.csproj

# Build
dotnet build WPF101/WPF101.csproj

# Run
dotnet run --project WPF101/WPF101.csproj
```

## 📝 What Should Happen (Step by Step)

### On Application Start:
1. App.xaml.cs creates IHost with DI
2. Registers all services (handlers, store, viewmodel)
3. Creates MainWindow with MainViewModel as DataContext
4. MainViewModel constructor:
   - Subscribes to store events
   - Calls LoadVehicles()
   - LoadVehicles queries database (empty initially)
5. Window shows with empty grid

### When Typing in Form:
1. TextBox binding updates NewMake/NewModel properties
2. OnPropertyChanged() fires
3. CommandManager.InvalidateRequerySuggested() fires
4. WPF calls CanAdd() on AddCommand
5. CanAdd() returns true if both Make and Model have text
6. Button becomes enabled

### When Clicking Add:
1. AddCommand.Execute() is called
2. AsyncRelayCommand calls AddVehicle()
3. Shows "Adding vehicle..." message
4. Creates CreateVehicleCommand
5. Calls _vehiclesStore.Add(command)
6. Store calls CreateVehicleCommandHandler.Handle()
7. Handler creates VehicleDto
8. Adds to DbContext
9. Calls SaveChangesAsync() → saves to database
10. Returns Vehicle with generated Id
11. Store fires VehicleAdded event
12. MainViewModel.OnVehicleAdded() adds to Vehicles collection
13. Grid updates automatically via binding
14. Shows "Vehicle added successfully!" message
15. Form fields clear

## 🎯 Quick Checklist

Before running, verify:
- [ ] Database exists (SQL Server Object Explorer)
- [ ] Migration applied (dbo.Vehicles table exists)
- [ ] Solution builds without errors
- [ ] appsettings.json has correct connection string
- [ ] App.xaml has correct namespace (WPF101.App)
- [ ] WPF101 is set as startup project

## 💡 Pro Tips

1. **Use Debug Mode**: Press F5 (not Ctrl+F5) para makita ang errors
2. **Check Output Window**: View → Output, select "Debug"
3. **Use Breakpoints**: Click left margin sa code para mag-stop
4. **Watch Variables**: Hover over variables in debug mode
5. **Check Error List**: View → Error List para sa compile errors

## 🆘 Last Resort

If nothing works, delete and recreate:

```powershell
# Delete database
dotnet ef database drop --project WPF101Framework --startup-project WPF101 --force

# Delete migrations
Remove-Item -Recurse -Force WPF101Framework\Migrations

# Recreate migration
dotnet ef migrations add InitialCreate --project WPF101Framework --startup-project WPF101

# Recreate database
dotnet ef database update --project WPF101Framework --startup-project WPF101

# Clean and rebuild
dotnet clean
dotnet build
```

Then run again with F5.
