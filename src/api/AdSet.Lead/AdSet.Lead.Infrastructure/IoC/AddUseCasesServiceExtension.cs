using Microsoft.Extensions.DependencyInjection;

namespace AdSet.Lead.Infrastructure.IoC;

public static class AddUseCasesServiceExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        // Vehicle
        /*services.AddScoped<CreateVehicle>();
        services.AddScoped<DeleteVehicle>();
        services.AddScoped<GetAllVehicles>();
        services.AddScoped<GetByIdVehicle>();
        services.AddScoped<UpdateVehicle>();
        */
        // Photo

        return services;
    }
}