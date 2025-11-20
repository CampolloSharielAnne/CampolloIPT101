using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF101.Stores;
using WPF101.ViewModels;
using WPF101.WPF101Framework.Commands;
using WPF101.WPF101Framework.Queries;

namespace WPF101.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<CreateVehicleCommandHandler>();
                services.AddSingleton<UpdateVehicleCommandHandler>();
                services.AddSingleton<DeleteVehicleCommandHandler>();
                services.AddSingleton<GetAllVehiclesQueryHandler>();

                services.AddSingleton<VehiclesStore>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton(s => new MainWindow
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });
            });

            return hostBuilder;
        }
    }
}
