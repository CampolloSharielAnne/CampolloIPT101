using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WPF101.WPF101Framework
{
    public class VehiclesDbContextDesignFactory : IDesignTimeDbContextFactory<VehiclesDbContext>
    {
        public VehiclesDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WPF101"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<VehiclesDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new VehiclesDbContext(optionsBuilder.Options);
        }
    }
}
