using RotaDeViagem.Application;
using RotaDeViagem.Application.Interfaces;
using RotaDeViagem.Infrastructure;
using RotaDeViagem.Infrastructure.Interfaces;

namespace RotaDeViagem
{
    public class DependencyInjection
    {
        public static void Register(IServiceCollection serviceProvider)
        {
            RepositoryDependence(serviceProvider);
        }

        private static void RepositoryDependence(IServiceCollection serviceProvider)
        {
            serviceProvider.AddScoped<IRotaService, RotaService>();
            serviceProvider.AddScoped<IRotaRepository, RotaRepository>();            
        }
    }
}
