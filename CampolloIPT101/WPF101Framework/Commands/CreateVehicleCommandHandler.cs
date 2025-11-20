using WPF101.WPF101Domain.Commands;
using WPF101.WPF101Domain.Models;
using WPF101.WPF101Framework.DTOs;

namespace WPF101.WPF101Framework.Commands
{
    public class CreateVehicleCommandHandler
    {
        private readonly VehiclesDbContextFactory _contextFactory;

        public CreateVehicleCommandHandler(VehiclesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Vehicle> Handle(CreateVehicleCommand command)
        {
            using var context = _contextFactory.Create();

            var vehicleDto = new VehicleDto
            {
                Make = command.Make,
                Model = command.Model,
                Year = command.Year,
                Price = command.Price
            };

            context.Vehicles.Add(vehicleDto);
            await context.SaveChangesAsync();

            return new Vehicle
            {
                Id = vehicleDto.Id,
                Make = vehicleDto.Make,
                Model = vehicleDto.Model,
                Year = vehicleDto.Year,
                Price = vehicleDto.Price
            };
        }
    }
}
