using AdSet.Data.Repositories; 

namespace adset_webapi.Configuration
{
    public static class DefaultServiceExtensions
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
            => services
            .AddContext()
            .AddUseCases()
            .AddRespositories();

        public static IServiceCollection AddContext(this IServiceCollection services)
            => services
            .AddDbContext<AdSetContext>(
                options =>
                {
                    options.UseSqlServer("name=ConnectionStrings:AdSetConnectionString");
                    options.EnableSensitiveDataLogging();
                }
            );

        public static IServiceCollection AddUseCases(this IServiceCollection services)
            => services
                .AddScoped<ISearchVehicles, SearchVehicles>()
                .AddScoped<ICreateVehicles, CreateVehicles>()
                .AddScoped<IDeleteVehicles, DeleteVehicles>()
                .AddScoped<IUpdateVehicles, UpdateVehicles>()
                .AddScoped<IUpdateVehiclePortalPackages, UpdateVehiclePortalPackages>()
                .AddScoped<IGetFilterOptions, GetFilterOptions>();


        public static IServiceCollection AddRespositories(this IServiceCollection services)
            => services
                .AddScoped<IVehiclesRepository, VehiclesRepository>()
                .AddScoped<IOptionalRepository, OptionalRepository>()
                .AddScoped<IVehicleOptionalRepository, VehicleOptionalRepository>()
                .AddScoped<IVehicleImageRepository, VehicleImageRepository>()
                .AddScoped<IVehiclePortalPackagesRepository, VehiclePortalPackagesRepository>()
                .AddScoped<IPortalRepository, PortalRepository>()
                .AddScoped<IPackagesRepository, PackagesRepository>();
    }
}
