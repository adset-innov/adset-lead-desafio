using AdSet.Lead.Domain.Repositories;
using AdSet.Lead.Infrastructure.Data.RepositoriesImpl;
using Microsoft.Extensions.DependencyInjection;

namespace AdSet.Lead.Infrastructure.IoC;

public static class AddRepositoriesServiceExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();

        return services;
    }
}