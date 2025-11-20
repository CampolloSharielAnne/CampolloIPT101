using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPF101.WPF101Domain.Commands;
using WPF101.WPF101Domain.Models;
using WPF101.WPF101Domain.Queries;
using WPF101.WPF101Framework.Commands;
using WPF101.WPF101Framework.Queries;

namespace WPF101.Stores
{
    public class VehiclesStore
    {
        private readonly CreateVehicleCommandHandler _createHandler;
        private readonly UpdateVehicleCommandHandler _updateHandler;
        private readonly DeleteVehicleCommandHandler _deleteHandler;
        private readonly GetAllVehiclesQueryHandler _getAllHandler;

        private List<Vehicle> _vehicles = new();

        public IEnumerable<Vehicle> Vehicles => _vehicles;

        public event Action? VehiclesLoaded;
        public event Action<Vehicle>? VehicleAdded;
        public event Action<Vehicle>? VehicleUpdated;
        public event Action<int>? VehicleDeleted;

        public VehiclesStore(
            CreateVehicleCommandHandler createHandler,
            UpdateVehicleCommandHandler updateHandler,
            DeleteVehicleCommandHandler deleteHandler,
            GetAllVehiclesQueryHandler getAllHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
        }

        public async Task Load()
        {
            var vehicles = await _getAllHandler.Handle(new GetAllVehiclesQuery());
            _vehicles = vehicles.ToList();
            VehiclesLoaded?.Invoke();
        }

        public async Task Add(CreateVehicleCommand command)
        {
            var vehicle = await _createHandler.Handle(command);
            _vehicles.Add(vehicle);
            VehicleAdded?.Invoke(vehicle);
        }

        public async Task Update(UpdateVehicleCommand command)
        {
            var vehicle = await _updateHandler.Handle(command);
            if (vehicle != null)
            {
                var index = _vehicles.FindIndex(v => v.Id == vehicle.Id);
                if (index >= 0)
                {
                    _vehicles[index] = vehicle;
                    VehicleUpdated?.Invoke(vehicle);
                }
            }
        }

        public async Task Delete(int id)
        {
            var success = await _deleteHandler.Handle(new DeleteVehicleCommand { Id = id });
            if (success)
            {
                _vehicles.RemoveAll(v => v.Id == id);
                VehicleDeleted?.Invoke(id);
            }
        }
    }
}
