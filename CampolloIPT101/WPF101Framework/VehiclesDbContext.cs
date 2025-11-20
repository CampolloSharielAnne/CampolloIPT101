using Microsoft.EntityFrameworkCore;
using WPF101.WPF101Framework.DTOs;

namespace WPF101.WPF101Framework
{
    public class VehiclesDbContext : DbContext
    {
        public VehiclesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<VehicleDto> Vehicles { get; set; }
    }
}
