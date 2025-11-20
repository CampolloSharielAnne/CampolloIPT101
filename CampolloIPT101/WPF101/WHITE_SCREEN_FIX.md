# White Screen Fix

## Problema: White Screen (Blank Window)

Ang window ay nag-show pero walang content (white screen lang).

## Possible Causes

### 1. DataContext is null
- Hindi na-set ang DataContext ng MainWindow
- DI issue

### 2. XAML not loading
- MainWindow.xaml.cs nawala or wrong namespace
- Build issue

### 3. Async loading issue
- LoadVehicles() nag-hang
- Database connection timeout

## ✅ Fixes Applied

1. **Recreated MainWindow.xaml.cs** - Nawala ito after autofix
2. **Added debug messages** - Para makita kung ano ang DataContext
3. **Added error handling** - Para makita ang errors

## 🔍 Testing Now

Pag nag-run mo ng app (F5), dapat may popup messages:

### Expected Messages:
1. **"DataContext type: MainViewModel"** - Kung OK ang DI
2. OR **"DataContext is null!"** - Kung may problem sa DI

### If "DataContext is null":

**Solution**: Check AddServicesHostBuilderExtensions.cs

Dapat ganito:
```csharp
services.AddSingleton<MainViewModel>();
services.AddSingleton(s => new MainWindow
{
    DataContext = s.GetRequiredService<MainViewModel>()
});
```

### If "DataContext type: MainViewModel" pero white screen pa rin:

**Possible Issue**: XAML bindings

**Solution A**: Check Output window
1. View → Output
2. Select "Debug" from dropdown
3. Look for binding errors like:
   - "Cannot find source for binding"
   - "BindingExpression path error"

**Solution B**: Simplify MainWindow.xaml temporarily

Replace content with simple test:
```xml
<Window x:Class="WPF101.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Motorshop - CRUD" Height="520" Width="800">
    <StackPanel>
        <TextBlock Text="Test" FontSize="24" Margin="10"/>
        <TextBlock Text="{Binding NewMake}" FontSize="18" Margin="10"/>
        <TextBox Text="{Binding NewMake, UpdateSourceTrigger=PropertyChanged}" Width="200" Margin="10"/>
    </StackPanel>
</Window>
```

If this shows "Test" and the textbox works, then the issue is with the original XAML.

## 🚀 Quick Test Steps

### Step 1: Run the app
```
Press F5
```

### Step 2: Check popup messages
- Should see "DataContext type: MainViewModel"
- If not, may DI issue

### Step 3: Check if window shows
- If white screen, check Output window for errors
- Look for XAML errors or binding errors

### Step 4: Test simple binding
- If DataContext is OK pero white screen pa rin
- Replace XAML with simple test above
- If simple test works, issue is sa complex XAML

## 🔧 Manual Fixes

### Fix 1: Rebuild Everything
```powershell
dotnet clean
dotnet build
```

### Fix 2: Check appsettings.json is copied
```powershell
# Check if file exists in output
dir WPF101\bin\Debug\net9.0-windows\appsettings.json
```

If not found:
1. Right-click appsettings.json in Solution Explorer
2. Properties
3. Copy to Output Directory: **Always**

### Fix 3: Remove async loading temporarily

In MainViewModel.cs constructor, comment out:
```csharp
// _ = LoadVehicles();
```

This removes the async database call on startup. If window shows after this, the issue is database connection timeout.

### Fix 4: Check database connection

Run in Package Manager Console:
```powershell
dotnet ef dbcontext info --project WPF101Framework --startup-project WPF101
```

Should show database info. If error, connection string issue.

## 🎯 Most Likely Issue

Based on "white screen" symptom, most likely:

1. **XAML not loading** - MainWindow.xaml.cs was deleted by autofix
   - ✅ FIXED: Recreated the file

2. **DataContext not set** - DI issue
   - Check with debug message

3. **Async hang** - LoadVehicles() nag-timeout
   - Comment out `_ = LoadVehicles();` to test

## 💡 Quick Diagnostic

Add this to MainWindow.xaml.cs:
```csharp
public MainWindow()
{
    InitializeComponent();
    MessageBox.Show("MainWindow constructor called", "Debug");
    
    this.Loaded += (s, e) =>
    {
        MessageBox.Show($"Window loaded. DataContext: {DataContext?.GetType().Name ?? "null"}", "Debug");
    };
}
```

This will show:
1. If constructor is called
2. If window loads
3. If DataContext is set

## 🆘 If Still White Screen

Try this minimal App.xaml.cs:
```csharp
protected override void OnStartup(StartupEventArgs e)
{
    try
    {
        var window = new MainWindow();
        window.DataContext = new MainViewModel(
            new VehiclesStore(
                new CreateVehicleCommandHandler(/* ... */),
                // ... other handlers
            )
        );
        window.Show();
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
    
    base.OnStartup(e);
}
```

This bypasses DI to test if the issue is with DI setup.
