using AdSet.Lead.Application.UseCases.Vehicle;
using Microsoft.Extensions.DependencyInjection;

namespace AdSet.Lead.Infrastructure.IoC;

public static class AddUseCasesServiceExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        // Vehicle
        services.AddScoped<AddOrUpdateVehiclePortalPackage>();
        services.AddScoped<CreateVehicle>();
        services.AddScoped<DeleteVehicle>();
        services.AddScoped<GetAllVehicles>();
        services.AddScoped<GetByIdVehicle>();
        services.AddScoped<GetDistinctColorsVehicle>();
        services.AddScoped<GetTotalCountVehicle>();
        services.AddScoped<GetWithoutPhotosCountVehicle>();
        services.AddScoped<GetWithPhotosCountVehicle>();
        services.AddScoped<RemoveVehiclePhoto>();
        services.AddScoped<RemoveVehiclePortalPackage>();
        services.AddScoped<SearchVehicles>();
        services.AddScoped<UpdateVehicle>();
        services.AddScoped<UploadVehiclePhoto>();

        return services;
    }
}