using Microsoft.EntityFrameworkCore;
using WPF101.WPF101Domain.Models;
using WPF101.WPF101Domain.Queries;

namespace WPF101.WPF101Framework.Queries
{
    public class GetAllVehiclesQueryHandler
    {
        private readonly VehiclesDbContextFactory _contextFactory;

        public GetAllVehiclesQueryHandler(VehiclesDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery query)
        {
            using var context = _contextFactory.Create();

            var vehicleDtos = await context.Vehicles.ToListAsync();

            return vehicleDtos.Select(dto => new Vehicle
            {
                Id = dto.Id,
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                Price = dto.Price
            });
        }
    }
}
