using Microsoft.EntityFrameworkCore;

namespace WPF101.WPF101Framework
{
    public class VehiclesDbContextFactory
    {
        private readonly DbContextOptions _options;

        public VehiclesDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public VehiclesDbContext Create()
        {
            return new VehiclesDbContext(_options);
        }
    }
}
