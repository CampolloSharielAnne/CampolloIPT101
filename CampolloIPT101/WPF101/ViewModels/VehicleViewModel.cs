using WPF101.WPF101Domain.Models;

namespace WPF101.ViewModels
{
    public class VehicleViewModel : ViewModelBase
    {
        private readonly Vehicle _vehicle;

        public int Id => _vehicle.Id;

        public string Make
        {
            get => _vehicle.Make;
            set
            {
                _vehicle.Make = value;
                OnPropertyChanged();
            }
        }

        public string Model
        {
            get => _vehicle.Model;
            set
            {
                _vehicle.Model = value;
                OnPropertyChanged();
            }
        }

        public int Year
        {
            get => _vehicle.Year;
            set
            {
                _vehicle.Year = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get => _vehicle.Price;
            set
            {
                _vehicle.Price = value;
                OnPropertyChanged();
            }
        }

        public VehicleViewModel(Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public void Update(Vehicle vehicle)
        {
            Make = vehicle.Make;
            Model = vehicle.Model;
            Year = vehicle.Year;
            Price = vehicle.Price;
        }
    }
}
