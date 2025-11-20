using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF101.WPF101Framework;

namespace WPF101.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                services.AddSingleton(new VehiclesDbContextFactory(
                    new DbContextOptionsBuilder().UseSqlServer(connectionString).Options));
            });

            return hostBuilder;
        }
    }
}
