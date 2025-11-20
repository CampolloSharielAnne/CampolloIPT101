using Microsoft.EntityFrameworkCore;
using WPF101.WPF101Domain.Commands;
using WPF101.WPF101Domain.Models;

namespace WPF101.WPF101Framework.Commands
{
    public class UpdateVehicleCommandHandler
    {
        private readonly VehiclesDbContextFactory _contextFactory;

        public UpdateVehicleCommandHandler(VehiclesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Vehicle?> Handle(UpdateVehicleCommand command)
        {
            using var context = _contextFactory.Create();

            var vehicleDto = await context.Vehicles.FindAsync(command.Id);
            if (vehicleDto == null) return null;

            vehicleDto.Make = command.Make;
            vehicleDto.Model = command.Model;
            vehicleDto.Year = command.Year;
            vehicleDto.Price = command.Price;

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
