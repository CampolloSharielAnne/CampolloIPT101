using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF101.Commands;
using WPF101.Stores;
using WPF101.WPF101Domain.Commands;

namespace WPF101.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly VehiclesStore _vehiclesStore;
        private VehicleViewModel? _selectedVehicle;
        private string _newMake = string.Empty;
        private string _newModel = string.Empty;
        private int _newYear;
        private decimal _newPrice;

        public ObservableCollection<VehicleViewModel> Vehicles { get; }

        public VehicleViewModel? SelectedVehicle
        {
            get => _selectedVehicle;
            set
            {
                _selectedVehicle = value;
                OnPropertyChanged();
            }
        }

        public string NewMake
        {
            get => _newMake;
            set
            {
                _newMake = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string NewModel
        {
            get => _newModel;
            set
            {
                _newModel = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public int NewYear
        {
            get => _newYear;
            set
            {
                _newYear = value;
                OnPropertyChanged();
            }
        }

        public decimal NewPrice
        {
            get => _newPrice;
            set
            {
                _newPrice = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public MainViewModel(VehiclesStore vehiclesStore)
        {
            _vehiclesStore = vehiclesStore;
            Vehicles = new ObservableCollection<VehicleViewModel>();

            AddCommand = new AsyncRelayCommand(AddVehicle, CanAdd);
            UpdateCommand = new AsyncRelayCommand(UpdateVehicle, CanUpdate);
            DeleteCommand = new AsyncRelayCommand(DeleteVehicle, CanDelete);
            RefreshCommand = new AsyncRelayCommand(LoadVehicles);

            _vehiclesStore.VehiclesLoaded += OnVehiclesLoaded;
            _vehiclesStore.VehicleAdded += OnVehicleAdded;
            _vehiclesStore.VehicleUpdated += OnVehicleUpdated;
            _vehiclesStore.VehicleDeleted += OnVehicleDeleted;

            _ = LoadVehicles();
        }

        private async Task LoadVehicles()
        {
            try
            {
                await _vehiclesStore.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnVehiclesLoaded()
        {
            Vehicles.Clear();
            foreach (var vehicle in _vehiclesStore.Vehicles)
            {
                Vehicles.Add(new VehicleViewModel(vehicle));
            }
        }

        private void OnVehicleAdded(WPF101Domain.Models.Vehicle vehicle)
        {
            Vehicles.Add(new VehicleViewModel(vehicle));
        }

        private void OnVehicleUpdated(WPF101Domain.Models.Vehicle vehicle)
        {
            var viewModel = Vehicles.FirstOrDefault(v => v.Id == vehicle.Id);
            viewModel?.Update(vehicle);
        }

        private void OnVehicleDeleted(int id)
        {
            var viewModel = Vehicles.FirstOrDefault(v => v.Id == id);
            if (viewModel != null)
            {
                Vehicles.Remove(viewModel);
            }
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(NewMake) && !string.IsNullOrWhiteSpace(NewModel);
        }

        private async Task AddVehicle()
        {
            try
            {
                MessageBox.Show($"Adding vehicle: {NewMake} {NewModel}", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);

                var command = new CreateVehicleCommand
                {
                    Make = NewMake,
                    Model = NewModel,
                    Year = NewYear,
                    Price = NewPrice
                };

                await _vehiclesStore.Add(command);

                MessageBox.Show("Vehicle added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                NewMake = string.Empty;
                NewModel = string.Empty;
                NewYear = 0;
                NewPrice = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding vehicle: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanUpdate()
        {
            return SelectedVehicle != null;
        }

        private async Task UpdateVehicle()
        {
            if (SelectedVehicle == null) return;

            try
            {
                var command = new UpdateVehicleCommand
                {
                    Id = SelectedVehicle.Id,
                    Make = SelectedVehicle.Make,
                    Model = SelectedVehicle.Model,
                    Year = SelectedVehicle.Year,
                    Price = SelectedVehicle.Price
                };

                await _vehiclesStore.Update(command);
                MessageBox.Show("Vehicle updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating vehicle: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanDelete()
        {
            return SelectedVehicle != null;
        }

        private async Task DeleteVehicle()
        {
            if (SelectedVehicle == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete {SelectedVehicle.Make} {SelectedVehicle.Model}?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _vehiclesStore.Delete(SelectedVehicle.Id);
                    SelectedVehicle = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting vehicle: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
