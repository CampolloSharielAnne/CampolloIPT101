using WPF101.WPF101Domain.Commands;

namespace WPF101.WPF101Framework.Commands
{
    public class DeleteVehicleCommandHandler
    {
        private readonly VehiclesDbContextFactory _contextFactory;

        public DeleteVehicleCommandHandler(VehiclesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> Handle(DeleteVehicleCommand command)
        {
            using var context = _contextFactory.Create();

            var vehicleDto = await context.Vehicles.FindAsync(command.Id);
            if (vehicleDto == null) return false;

            context.Vehicles.Remove(vehicleDto);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
