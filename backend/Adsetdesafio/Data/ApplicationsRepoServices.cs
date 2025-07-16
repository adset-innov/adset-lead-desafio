using Adsetdesafio.Application.Services.CarServices;
using Adsetdesafio.Domain.Infraestructure.Interfaces;
using Adsetdesafio.Domain.Infraestructure.Repositories;

namespace Adsetdesafio.Data
{
    public static class ApplicationsRepoServices
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICarRepository, CarsRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(PutCarsService));
            services.AddScoped(typeof(ValidateCarsService));
            services.AddScoped(typeof(GetCarsService));
            services.AddScoped(typeof(DeleteCarsService));
        }
    }
}
